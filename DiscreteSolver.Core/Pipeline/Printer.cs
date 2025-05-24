using DiscreteSolver.Core.Structs;

namespace DiscreteSolver.Core.Pipeline
{
    class Printer
    {
        readonly List<SimplificationDescription> lines = new List<SimplificationDescription>();

        internal SimplificationDescription Add(Substitution value)
        {
            var appliedRulePresent = !(value.InitialPart is null || value.ResultingPart is null);
            var simp = new SimplificationDescription
            {
                SimplifiedExpression = value.ResultingExpression?.ToString() ?? string.Empty,
                AppliedRule = appliedRulePresent ? $"{value.InitialPart} => {value.ResultingPart}" : string.Empty,
                RuleDescription = value.Description
            };
            lines.Add(simp);
            return simp;
        }

        internal List<SimplificationDescription> GetLines()
        {
            return lines;
        }
    }
}