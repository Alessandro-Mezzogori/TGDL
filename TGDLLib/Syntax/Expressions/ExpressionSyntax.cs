namespace TGDLLib.Syntax;
public enum OperationKind
{
    // binary operations
    Addition,
    Subtraction,
    Moltiplication,
    Division,
    Power,
    Modulo,
    GreaterThan,
    LessThan,
    GreaterOrEqual,
    LessOrEqual,
    Equal,
    NotEqual,
    Not,
    And,
    Or,

    // unary operations
    Plus,
    Minus,
    
    // other expression types
    Literal,
    Identifier,
    Dot,
}

public abstract class ExpressionSyntax
{
    public OperationKind Operation { get; }
    public TGDLType? Type { get; private set; } 

    public ExpressionSyntax(OperationKind operation, TGDLType? type = null)
    {
        Operation = operation;
        Type = type;
    }

    protected void SetType(TGDLType type) => Type = Type;
}
