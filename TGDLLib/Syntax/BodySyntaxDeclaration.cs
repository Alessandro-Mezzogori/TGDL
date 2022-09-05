namespace TGDLLib.Syntax;

public class BodySyntaxDeclaration
{
    
    // Contains Statements:
    // - IfStatement
    // - ReturnStatement
    // - ExpressionStament
    // Statements contain Expressions
    // An expression can be written inside another expression
    // - MemberAccessExpression
    // - AssignmentExpression
    // - OperationExpression
    // - InvocationExpression
    IEnumerable<StatementSyntax> Statements { get; }

    public BodySyntaxDeclaration(IEnumerable<StatementSyntax> statements)
    {
        Statements = statements;
    }
}
