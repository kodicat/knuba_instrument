using DiscreteSolver.Core.Structs;
using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace DiscreteSolver.Core.Language
{
    internal interface IProvideTokenizer
    {
        Tokenizer<TokenType> GetTokenizer();
    }

    public class Syntax : IProvideTokenizer
    {
        private readonly TokenizerBuilder<TokenType> syntaxBuilder;

        public Syntax(ISettings settings)
        {
            syntaxBuilder = new SyntaxBuilder(settings).Builder;
        }

        public Tokenizer<TokenType> GetTokenizer() => syntaxBuilder.Build();
    }

    internal class SyntaxWithVariables : IProvideTokenizer
    {
        private readonly TokenizerBuilder<TokenType> syntaxBuilder;

        public SyntaxWithVariables(ISettings settings)
        {
            var defaultSettings = new DefaultSettings();
            syntaxBuilder = new SyntaxBuilder(settings).Builder;
        }

        public Tokenizer<TokenType> GetTokenizer()
            => syntaxBuilder
                .Match(Span.Regex("_[A-E]"), TokenType.Variable)
                .Build();
    }

    internal class SyntaxBuilder
    {
        internal TokenizerBuilder<TokenType> Builder { get; }

        internal SyntaxBuilder(ISettings settings)
        {
            Builder =
                new TokenizerBuilder<TokenType>()
                .Ignore(Span.WhiteSpace)
                .Match(Character.In(settings.Sets), TokenType.Set)
                .Match(Character.In(settings.Unions), TokenType.Union)
                .Match(Character.In(settings.Intersections), TokenType.Intersection)
                .Match(Character.In(settings.Differences), TokenType.Difference)
                .Match(Character.In(settings.SymmetricDifferences), TokenType.SymmetricDifference)
                .Match(Character.In(settings.PrefixNegations), TokenType.PrefixNegation)
                .Match(Character.In(settings.PostfixNegations), TokenType.PostfixNegation)
                .Match(Character.In(settings.LParens), TokenType.LParen)
                .Match(Character.In(settings.RParens), TokenType.RParen)
                .Match(Character.In(settings.UniverseSets), TokenType.UniverseSet)
                .Match(Character.In(settings.EmptySets), TokenType.EmptySet);
        }
    }
}