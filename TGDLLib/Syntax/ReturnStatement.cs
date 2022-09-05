namespace TGDLLib.Syntax;

public class ReturnStatement : StatementSyntax
{
    // One Expression
    public ExpressionSyntax Expression { get; }
    public ReturnStatement(ExpressionSyntax expression)
    {
        Expression = expression;
    }
}
