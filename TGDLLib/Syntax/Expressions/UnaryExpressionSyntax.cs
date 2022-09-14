namespace TGDLLib.Syntax;

public class UnaryExpressionSyntax : ExpressionSyntax
{   
    public ExpressionSyntax Operand { get; }

    public UnaryExpressionSyntax(ExpressionSyntax operand, OperationKind operation)
        : base(operation)
    {
        Operand = operand;
    }
}

