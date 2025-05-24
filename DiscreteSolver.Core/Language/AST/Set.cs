namespace DiscreteSolver.Core.Language.AST
{
    public class Set : Expression
    {
        public Set(string value)
        {
            Value = value;
            Children = [];
            Id = Guid.NewGuid();
        }

        private Set(string value, Guid id)
        {
            Value = value;
            Children = [];
            Id = id;
        }

        public override string Value { get; }
        public override Expression[] Children { get; set; }

        public override Expression Clone() => new Set(Value, Id);
        public override Expression Copy() => new Set(Value);

        public override string ToString() => Value;
    }
}