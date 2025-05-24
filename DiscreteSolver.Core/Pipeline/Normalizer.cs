using DiscreteSolver.Core.Language.AST;
using DiscreteSolver.Core.Utils;

namespace DiscreteSolver.Core.Pipeline
{
    internal static class Normalizer
    {
        internal static Expression Normalize(this Expression expr)
        {
            return expr
                .CombineCommutativeOperators()
                .Sort();
        }

        private static Expression CombineCommutativeOperators(this Expression expr)
        {
            return expr
                .AsTree()
                .ChangeTree((x, parent) => x.IsIntersection() && parent.IsIntersection(), x => x.Children)
                .ChangeTree((x, parent) => x.IsUnion() && parent.IsUnion(), x => x.Children)
                .ChangeTree((x, parent) => x.IsSymmetricDifference() && parent.IsSymmetricDifference(), x => x.Children)
                .AsExpression();
        }

        private static Expression Sort(this Expression expr)
        {
            return expr
                .AsTree()
                .OrderBy(x => x.IsIntersection() || x.IsUnion() || x.IsSymmetricDifference(), x => x.ToString())
                .AsExpression();
        }
    }
}