using TGDLLib.Syntax.Expressions;

namespace TGDLLib.Syntax.Statements
{
    internal class AssignmentStatementSyntax : StatementSyntax
    {
        public IdentifierNameExpressionSyntax Identifier { get; }
        public ExpressionSyntax Expression { get; }

        public AssignmentStatementSyntax(IdentifierNameExpressionSyntax identifier, ExpressionSyntax expression)
        {
            Identifier = identifier;
            Expression = expression;
        }
    }
}
