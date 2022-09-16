using Antlr4.Runtime.Misc;
using TGDLLib.Syntax;

namespace TGDLLib.Visitors;

internal class TypeVisitor : TGDLBaseVisitor<TypeSyntax>
{
    public override TypeSyntax VisitType([NotNull] TGDLParser.TypeContext context)
    {
        return context.token.Type switch
        {
            TGDLLexer.BOOL_TYPE         => SyntaxFactory.PredefinedType(TGDLType.Bool),
            TGDLLexer.DECIMAL_TYPE      => SyntaxFactory.PredefinedType(TGDLType.Decimal),
            TGDLLexer.STRING_TYPE       => SyntaxFactory.PredefinedType(TGDLType.String),
            TGDLLexer.BOARD_TYPE        => SyntaxFactory.SuppliedPredefinedType(TGDLType.Board),
            TGDLLexer.BOARDCELL_TYPE    => SyntaxFactory.SuppliedPredefinedType(TGDLType.BoardCell),
            TGDLLexer.PLAYER_TYPE       => SyntaxFactory.SuppliedPredefinedType(TGDLType.Player),
            TGDLLexer.IDENTIFIER        => SyntaxFactory.DeclaredType(context.token.Text),
            _ => throw new ParseException(typeof(TypeSyntax), "type not found")
        };
    }
}

internal class ParameterVisitor : TGDLBaseVisitor<ParameterSyntaxDeclaration>
{
    private readonly TypeVisitor _typeVisitor = new();
    public override ParameterSyntaxDeclaration VisitParameter([NotNull] TGDLParser.ParameterContext context)
    {
        var typeContext = context.type();
        if (typeContext == null)
            throw new ParseException("type not found in parameter");
        var type = _typeVisitor.Visit(typeContext);

        var identifierNode = context.IDENTIFIER();
        if (identifierNode == null)
            throw new ParseException("parameter identifier not found");
        var identifier = identifierNode.GetText();

        return SyntaxFactory.Parameter(type, SyntaxFactory.Identifier(identifier));
    }
}

internal class LambdaVisitor : TGDLBaseVisitor<LambdaSyntaxDeclaration>
{
    private readonly ParameterVisitor _parameterVisitor = new();
    private readonly BodyVisitor _bodyVisitor = new();
    public override LambdaSyntaxDeclaration VisitLambda([NotNull] TGDLParser.LambdaContext context)
    {
        var parameter = context.parameter().Select(x => _parameterVisitor.Visit(x));

        var body = _bodyVisitor.Visit(context.body());
        if (body == null)
            throw new ParseException("lambda body not found");

        return SyntaxFactory.Lambda(body, parameter);
    }
}
