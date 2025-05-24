using DiscreteSolver.Core.Language;
using DiscreteSolver.Core.Language.AST;
using DiscreteSolver.Core.Utils;

namespace DiscreteSolver.Tests.Core
{
    [TestClass]
    public class TreeTraversalFixture
    {
        private readonly IProvideTokenizer syntax;
        private readonly Grammar grammar;
        private readonly ISettings settings;

        public TreeTraversalFixture()
        {
            settings = new DefaultSettings();
            syntax = new Syntax(settings);
            grammar = new Grammar(settings);
        }

        [DataTestMethod]
        [DataRow("A", "A'")]
        [DataRow("A + B", "(A' + B')'")]
        [DataRow("(A * B) + (C * D)", "((A' * B')' + (C' * D')')'")]
        public void ChangeTreeShoudComplementAllTreeMembers(string input, string output)
        {
            var tokenizer = syntax.GetTokenizer();
            var tokensResult = tokenizer.TryTokenize(input);
            var parseResult = grammar.BuildTree(tokensResult.Value);

            var expr = parseResult.Value;
            var actual = expr.SubstituteNode(x => true, x => new Negation("'", x));

            var expectedTokens = tokenizer.TryTokenize(output).Value;
            var expected = grammar.BuildTree(expectedTokens).Value;

            var equal = TreeUtils.StrictEquals(expected, actual);

            Assert.IsTrue(equal, "The tree is not complemented");
        }

        [DataTestMethod]
        [DataRow("A'", "A")]
        [DataRow("(A' + B')'", "A + B")]
        [DataRow("((A' * B')' + (C' * D')')'", "(A * B) + (C * D)")]
        public void ChangeTreeShoudRemoveAllComplements(string input, string output)
        {
            var tokenizer = syntax.GetTokenizer();
            var tokensResult = tokenizer.TryTokenize(input);
            var parseResult = grammar.BuildTree(tokensResult.Value);

            var expr = parseResult.Value;
            var actual = expr.SubstituteNode(x => x.Type == typeof(Negation), x => x.Children[0]);

            var expectedTokens = tokenizer.TryTokenize(output).Value;
            var expected = grammar.BuildTree(expectedTokens).Value;

            var equal = TreeUtils.StrictEquals(expected, actual);

            Assert.IsTrue(equal, "The tree is not complemented");
        }
    }
}