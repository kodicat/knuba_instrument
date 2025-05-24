﻿namespace DiscreteSolver.Core.Language.AST
{
    public class Variable : Expression
    {
        public Variable(string value)
        {
            Value = value;
            Children = Array.Empty<Expression>();
        }

        public override string Value { get; }
        public override Expression[] Children { get; set; }

        public override Expression Clone() => new Variable(Value);
        public override Expression Copy() => new Variable(Value);
        public override string ToString() => Value;
    }
}