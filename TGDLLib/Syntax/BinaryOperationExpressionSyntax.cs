namespace TGDLLib.Syntax;

public enum OperatorKind
{
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
    NotEqual
}

public class BinaryOperationExpressionSyntax : ExpressionSyntax
{
    public ExpressionSyntax LeftOperand { get; }
    public OperatorKind Operation { get; }
    public ExpressionSyntax RightOperand { get; }
    
    public BinaryOperationExpressionSyntax(ExpressionSyntax leftOperand, OperatorKind operation, ExpressionSyntax rightOperand)
    {
        LeftOperand = leftOperand;
        Operation = operation;
        RightOperand = rightOperand;
    }
}

