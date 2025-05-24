namespace DiscreteSolver.Core.Language.AST
{
    public class Tree : Expression
    {
        private const string TreeValue = "Tree";

        public Tree(Expression child)
        {
            Value = TreeValue;
            Children = new[] { child };
        }

        public override string Value { get; }
        public override Expression[] Children { get; set; }

        public override Expression Clone() => new Tree(Children[0].Clone());
        public override Expression Copy() => new Tree(Children[0].Copy());
        public override string ToString() => $"{Children[0]} ";

        public override Tree AsTree() => this;
    }
}