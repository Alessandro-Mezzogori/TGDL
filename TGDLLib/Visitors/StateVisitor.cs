using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Statements;

namespace TGDLLib.Visitors;


internal class StateVisior : TGDLBaseVisitor<StateSyntaxDeclaration>
{
    private readonly StatementVisitor _statementVisitor = new();
    private readonly LambdaVisitor _lambdaVisitor = new();

    public override StateSyntaxDeclaration VisitState([NotNull] TGDLParser.StateContext context)
    {
        var scope = ParseStateScope(context.scope);
        var identifier = context.IDENTIFIER().GetText();

        List<AssignmentStatementSyntax> attributes = new();
        if(context.statement() != null)
        {
            foreach(var statement in context.statement())
            {
                var visited = _statementVisitor.Visit(statement);

                if (visited is not AssignmentStatementSyntax assignmentStatement)
                    throw new ParseException("state attributes must be an assignment statement");

                attributes.Add(assignmentStatement);
            }
        }

        List<LambdaSyntaxDeclaration> actions = new();
        if (context.action() != null)
        {
            foreach(var action in context.action())
            {
                var visited = _lambdaVisitor.Visit(action);

                if(!visited.ReturnType.IsPredifinedType && ((PredefinedTypeSyntax)visited.ReturnType).Type != TGDLType.Void)
                {
                    // throw new ParseException("Action Effect return type must be void")
                }

                actions.Add(visited);
            }
        }

        return SyntaxFactory.State(
            SyntaxFactory.Identifier(identifier),
            SyntaxFactory.StateScope(scope),
            attributes
            // actions
        );
    }

    private StateScope ParseStateScope(IToken? scope)
    {
        if (scope == null) 
            return StateScope.Local;

        return scope.Type switch
        {
            TGDLLexer.LOCAL => StateScope.Local,
            TGDLLexer.GLOBAL => StateScope.Global,
            TGDLLexer.GROUP => StateScope.Group,
            _ => throw new ParseException($"{scope.Text} is not a state scope modifer token")
        };
    }
}
