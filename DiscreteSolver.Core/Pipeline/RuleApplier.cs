using DiscreteSolver.Core.Language;
using DiscreteSolver.Core.Language.AST;
using DiscreteSolver.Core.Utils;

namespace DiscreteSolver.Core.Pipeline
{
    internal class RuleApplier
    {
        private static readonly Cache<Guid, string, Expression> cache = new Cache<Guid, string, Expression>();

        internal Expression ApplyRuleWithCache(Expression expression, Rule rule)
            => cache.GetCachedOrExecute(rule.Id, expression.ToString(), () => ApplyRule(expression, rule));

        private Expression ApplyRule(Expression expression, Rule rule)
        {
            var variables = new Dictionary<string, Expression>();

            if (CheckMatch(expression, rule.PatternIn, variables))
                return rule.PatternOut.SubstituteNode(x => x.IsVariable(), x => variables[x.ToString()].Copy());

            return null;
        }

        private static bool CheckMatch(Expression toBeMatched, Expression pattern, Dictionary<string, Expression> variables)
        {
            if (pattern.IsVariable())
            {
                var exists = variables.TryGetValue(pattern.Value, out var substitution);
                if (exists)
                    return toBeMatched.StrictEquals(substitution);

                variables[pattern.ToString()] = toBeMatched.Copy();
                return true;
            }

            if (!toBeMatched.IsSameExpression(pattern))
                return false;

            if (!pattern.HasChildren())
                return true;

            if (toBeMatched.Children.Length < pattern.Children.Length)
                return false;

            return toBeMatched.Children.PairsComply(pattern.Children, (a, b) => CheckMatch(a, b, variables));
        }
    }
}