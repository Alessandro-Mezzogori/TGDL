using Sprache;
using TGDLLib.Syntax;

namespace TGDLLib;

internal static class Parsers
{
    // Action Parsers
    public static readonly Parser<IdentifierSyntaxToken> IdentifierToken = 
        Parse.Letter.AtLeastOnce().Text().Token().Select(x => new IdentifierSyntaxToken(x));

    public static readonly Parser<TypeSyntaxToken> TypeToken =
        Parse.Letter.AtLeastOnce().Text().Token().Select(x => new TypeSyntaxToken(x));

    // TODO Rivedere la sintassi per il corpo delle azioni / Funzioni
    public static readonly Parser<BodySyntaxDeclaration> BodySyntax =
        Parse.AnyChar.AtLeastOnce().Text().Select(x => new BodySyntaxDeclaration());

    public static readonly Parser<ParameterSyntaxDeclaration> ParameterSyntax =
        from type in TypeToken
        from identifier in IdentifierToken
        select new ParameterSyntaxDeclaration(type, identifier);

    public static readonly Parser<IEnumerable<ParameterSyntaxDeclaration>> ParametersSyntax =
        ( from parameter in ParameterSyntax
          from delimiter in Parse.Char(TokenConstants.ParameterDelimiterToken)
          select parameter).AtLeastOnce().Select(x => x.ToList());

    public static readonly Parser<RequireLambdaExpressionDeclaration> RequireLambdaExpression =
        from parameters in ParametersSyntax.Optional()
        from delimiter in Parse.String(TokenConstants.LambdaBodyDelimiter)
        from openBlock in Parse.Char(TokenConstants.BlockStart)
        from body in BodySyntax
        select new RequireLambdaExpressionDeclaration(parameters.GetOrElse(Enumerable.Empty<ParameterSyntaxDeclaration>()), body);

    public static readonly Parser<RequireSyntaxDeclaration> RequireSyntax =
        from requireToken in Parse.String(TokenConstants.RequireToken)
        from lambdas in RequireLambdaExpression.AtLeastOnce()
        select new RequireSyntaxDeclaration(lambdas);

    public static readonly Parser<GameActionSyntaxDeclaration> GameActionSyntaxDeclaration =
        from actionToken in Parse.String(TokenConstants.ActionToken)
        from identifier in IdentifierToken
        from openBlcok in Parse.Char(TokenConstants.BlockStart)
        from require in RequireSyntax.Optional()
        select new GameActionSyntaxDeclaration(identifier, require.GetOrDefault());
        
}
