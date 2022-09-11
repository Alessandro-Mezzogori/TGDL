namespace TGDLLib.Syntax;

public class ReturnStatementSyntax : StatementSyntax
{
    // One Expression
    public ExpressionSyntax Expression { get; }
    public ReturnStatementSyntax(ExpressionSyntax expression)
    {
        Expression = expression;
    }
}
