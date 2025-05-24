using DiscreteSolver.Core.Language.AST;

namespace DiscreteSolver.Core.Structs
{
    public class Substitution
    {
        public Expression InitialExpression { get; set; }
        public Expression ResultingExpression { get; set; }
        public Expression InitialPart { get; set; }
        public Expression ResultingPart { get; set; }
        public string Description { get; set; }
    }
}