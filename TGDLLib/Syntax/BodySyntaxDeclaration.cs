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
    public IEnumerable<StatementSyntax> Statements { get; }

    public BodySyntaxDeclaration(IEnumerable<StatementSyntax> statements)
    {
        Statements = statements;
    }
}

public class SingleLineBodySyntaxDeclaration : BodySyntaxDeclaration
{
    public SingleLineBodySyntaxDeclaration(ReturnStatementSyntax returnStatement) 
        : base(new ReturnStatementSyntax[] {returnStatement})
    {
    }
}
