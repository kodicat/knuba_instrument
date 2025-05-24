﻿using DiscreteSolver.Core.Language;
using DiscreteSolver.Core.Structs;
using Superpower.Model;

namespace DiscreteSolver.Tests.Core
{
    [TestClass]
    public class SyntaxFixture
    {
        private readonly IProvideTokenizer syntax;

        public SyntaxFixture()
        {
            var settings = new DefaultSettings();
            syntax = new Syntax(settings);
        }

        [DataTestMethod]
        [DataRow(" +∪∨| ")]
        [DataRow(" *∩∧& ")]
        [DataRow(" ABCDE ")]
        [DataRow(" U1 ")]
        [DataRow(" O0∅ ")]
        [DataRow(@" \- ")]
        [DataRow(" ) ")]
        [DataRow(" ( ")]
        [DataRow(" ! ")]
        [DataRow(" ' ")]
        public void TokenizerReturnsResultOnValidInputPerLexema(string input)
        {
            var result = syntax.GetTokenizer().TryTokenize(input);
            Assert.IsTrue(result.HasValue);
        }

        [DataTestMethod]
        [DataRow(@"+*AUO\()!'")]
        [DataRow("∪∩B10-()!'")]
        [DataRow(@"∨∧CU0\()!'")]
        [DataRow("|&D1O-()!'")]
        [DataRow(@"+*EUO\()!'")]
        [DataRow(@" + * A U O \ ( ) ! ' ")]
        [DataRow("  ∪  ∩  B  1  0  -  (  )  !  '  ")]
        [DataRow(@" ∨ ∧ C U 0 \ ( ) ! ' ")]
        [DataRow(" | & D 1 O - ( ) ! ' ")]
        [DataRow(@"  +  *  E  U  O  \  (  )  !  '  ")]
        public void TokenizerReturnsResultOnValidInput(string input)
        {
            var result = syntax.GetTokenizer().TryTokenize(input);
            Assert.IsTrue(result.HasValue);
        }

        [DataTestMethod]
        [DataRow("_")]
        [DataRow("_1")]
        [DataRow("2")]
        [DataRow("%")]
        public void TokenizerReturnsErrorOnInvalidInput(string input)
        {
            var result = syntax.GetTokenizer().TryTokenize(input);
            Assert.IsFalse(result.HasValue);
            Assert.AreEqual(Position.Zero, result.ErrorPosition);
        }

        [TestMethod]
        public void TokenizerReturnsExpectedTokenList()
        {
            var input = @"A ∪ (!B ∩ A') \ A △ B ∩ U ∪ ∅";
            var expected = new List<TokenType>
            {
                TokenType.Set,
                TokenType.Union,
                TokenType.LParen,
                TokenType.PrefixNegation,
                TokenType.Set,
                TokenType.Intersection,
                TokenType.Set,
                TokenType.PostfixNegation,
                TokenType.RParen,
                TokenType.Difference,
                TokenType.Set,
                TokenType.SymmetricDifference,
                TokenType.Set,
                TokenType.Intersection,
                TokenType.UniverseSet,
                TokenType.Union,
                TokenType.EmptySet
            };
            var result = syntax.GetTokenizer().TryTokenize(input);
            var actual = result.Value.Select(x => x.Kind).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}