namespace TGDLLib.Syntax.Statements;

public class ExpressionStatement : StatementSyntax
{
    public ExpressionSyntax Expression { get; }

    public ExpressionStatement(ExpressionSyntax expression)
    {
        Expression = expression;
    }
}
