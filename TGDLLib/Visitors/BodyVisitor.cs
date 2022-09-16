using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using TGDLLib.Syntax;

namespace TGDLLib.Visitors;

internal class BodyVisitor : TGDLBaseVisitor<BodySyntaxDeclaration>
{
    private readonly StatementVisitor _stamentVisitor = new();

    public override BodySyntaxDeclaration VisitBody([NotNull] TGDLParser.BodyContext context)
    {
        var statements = context.statement();

        var statementsSyntax = statements.Select(x => _stamentVisitor.Visit(x));

        return SyntaxFactory.Body(statementsSyntax);
    }

    public override BodySyntaxDeclaration Visit([NotNull] IParseTree tree)
    {
        return base.Visit(tree);
    }
}
