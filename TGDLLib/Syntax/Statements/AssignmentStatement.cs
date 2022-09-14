using TGDLLib.Syntax.Expressions;

namespace TGDLLib.Syntax.Statements;

public class AssignmentStatementSyntax : StatementSyntax
{
    public ExpressionSyntax Identifier { get; }
    public ExpressionSyntax Expression { get; }

    public AssignmentStatementSyntax(ExpressionSyntax identifier, ExpressionSyntax expression)
    {
        Identifier = identifier;
        Expression = expression;
    }
}
