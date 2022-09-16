using Antlr4.Runtime.Misc;
using TGDLLib.Syntax;

namespace TGDLLib.Visitors;

internal class StatementVisitor : TGDLBaseVisitor<StatementSyntax>
{
    private readonly ExpressionVisitor _expressionVisitor = new();

    public override StatementSyntax VisitReturnStament([NotNull] TGDLParser.ReturnStamentContext context)
    {
        var expression = _expressionVisitor.Visit(context.expression()); 

        return SyntaxFactory.Return(expression);
    }

    public override StatementSyntax VisitAssignmentStatement([NotNull] TGDLParser.AssignmentStatementContext context)
    {
        var left = _expressionVisitor.Visit(context.expression(0)); 
        var right = _expressionVisitor.Visit(context.expression(1));
        return SyntaxFactory.Assignment(left, right);
    }

    public override StatementSyntax VisitExpressionStatement([NotNull] TGDLParser.ExpressionStatementContext context)
    {
        var expression = _expressionVisitor.Visit(context.expression());
        return SyntaxFactory.Expression(expression);
    }
}
