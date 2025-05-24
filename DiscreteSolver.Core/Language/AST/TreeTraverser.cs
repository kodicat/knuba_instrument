namespace DiscreteSolver.Core.Language.AST
{
    internal static class TreeTraverser
    {
        internal static IEnumerable<Expression> AsEnumerable(this Expression expr)
        {
            foreach (var child in expr.Children)
                foreach (var subChild in child.AsEnumerable())
                    yield return subChild;
            yield return expr;
        }


        internal static Tree ChangeTree(this Tree tree, Func<Expression, bool> predicate, Func<Expression, Expression> func)
        {
            var copy = tree.Clone();
            copy.DepthFirstSearchPostOrder(predicate, func);
            return (Tree)copy;
        }

        internal static Tree ChangeTree(
            this Tree tree,
            Func<Expression, Expression, bool> predicate,
            Func<Expression, IEnumerable<Expression>> func)
        {
            var copy = tree.Clone();
            copy.DepthFirstSearchPostOrder(predicate, func);
            return (Tree)copy;
        }

        internal static Tree OrderBy<T>(
            this Tree tree,
            Func<Expression, bool> predicate,
            Func<Expression, T> selector)
        {
            var copy = tree.Clone();
            copy.DepthFirstSearchPostOrder(predicate, selector);
            return (Tree)copy;
        }

        public static Expression AsExpression(this Tree tree) => tree.Children[0];

        private static void DepthFirstSearchPostOrder(this Expression expr, Func<Expression, bool> predicate, Func<Expression, Expression> func)
        {
            foreach (var child in expr.Children)
                child.DepthFirstSearchPostOrder(predicate, func);

            expr.Children = expr.Children.Select(x => predicate(x) ? func(x) : x).ToArray();
        }

        private static void DepthFirstSearchPostOrder(
            this Expression expr,
            Func<Expression, Expression, bool> predicate,
            Func<Expression, IEnumerable<Expression>> func)
        {
            foreach (var child in expr.Children)
                child.DepthFirstSearchPostOrder(predicate, func);

            expr.Children = expr.Children.SelectMany(x => predicate(x, expr) ? func(x).ToArray() : new[] { x }).ToArray();
        }

        private static void DepthFirstSearchPostOrder<T>(
            this Expression expr,
            Func<Expression, bool> predicate,
            Func<Expression, T> selector)
        {
            foreach (var child in expr.Children)
                child.DepthFirstSearchPostOrder(predicate, selector);

            if (predicate(expr))
                expr.Children = expr.Children.OrderBy(selector).ToArray();
        }
    }
}
