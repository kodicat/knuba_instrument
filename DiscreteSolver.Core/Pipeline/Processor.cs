﻿using DiscreteSolver.Core.Language;
using DiscreteSolver.Core.Structs;
using DiscreteSolver.Core.Utils;

namespace DiscreteSolver.Core.Pipeline
{
    public class Processor
    {
        public static MyResult<List<SimplificationDescription>> Process(string input, ISettings settings)
        {
            var combinedSettings = settings.Combine(new DefaultSettings());
            var rules = new Rules();

            IProvideTokenizer syntax = new Syntax(combinedSettings);
            var tokenizer = syntax.GetTokenizer();
            var tokensResult = tokenizer.TryTokenize(input);
            if (!tokensResult.HasValue)
            {
                var errorIndex = GetErrorIndex(tokensResult.ErrorPosition);
                var token = tokensResult.Remainder.First(1).ToStringValue();
                // use localized user error messages
                return new MyResult<List<SimplificationDescription>>($"Syntax error for input '{token}'", errorIndex, token);
            }

            var grammar = new Grammar(combinedSettings);
            var parseResult = grammar.BuildTree(tokensResult.Value);
            if (!parseResult.HasValue)
            {
                var errorIndex = GetErrorIndex(parseResult.ErrorPosition);
                var token = parseResult.Remainder.ConsumeToken().Value.ToStringValue();
                // use localized user error messages
                return new MyResult<List<SimplificationDescription>>($"Syntax error for input '{token}'", errorIndex, token);
            }

            // work with errors
            var simplifier = new Simplifier(
                new PatternMatcher(rules.GetRules(), new RuleApplier()),
                new Printer());
            var result = simplifier.Run(parseResult.Value);
            return result;
        }

        static int GetErrorIndex(Superpower.Model.Position position)
        {
            return position.HasValue && position.Line == 1
                ? position.Column - 1
                : -1;
        }
    }
}