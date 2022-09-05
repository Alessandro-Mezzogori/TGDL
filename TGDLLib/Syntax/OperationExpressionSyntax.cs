namespace TGDLLib.Syntax;

public enum Operation
{
    Addition,
    Subtraction,
    Moltiplication,
    Division,
    Power,
    Modulo
}

public class OperationExpressionSyntax : ExpressionSyntax
{
    public ExpressionSyntax LeftOperand { get; }
    public Operation Operation { get; }
    public ExpressionSyntax RightOperand { get; }
    
    public OperationExpressionSyntax(ExpressionSyntax leftOperand, Operation operation, ExpressionSyntax rightOperand)
    {
        LeftOperand = leftOperand;
        Operation = operation;
        RightOperand = rightOperand;
    }
}
