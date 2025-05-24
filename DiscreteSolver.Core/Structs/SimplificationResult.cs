using DiscreteSolver.Core.Language.AST;

namespace DiscreteSolver.Core.Structs
{
    internal class Simplification
    {
        public List<Substitution> Substitutions { get; set; }
        public Expression ResultedExpression { get; set; }
    }
}