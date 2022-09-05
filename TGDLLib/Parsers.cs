using Sprache;
using TGDLLib.Syntax;

namespace TGDLLib;

internal static class Parsers
{
    internal static class Tokens
    {
        // Action Parsers
        public static readonly Parser<IdentifierSyntaxToken> Identifier =
            Parse.Letter.AtLeastOnce().Text().Token().Select(x => new IdentifierSyntaxToken(x));

        public static readonly Parser<TypeSyntaxToken> Type =
            Parse.Letter.AtLeastOnce().Text().Token().Select(x => new TypeSyntaxToken(x));

    }

    internal static class Operators
    {
        // Left Associative 
        public static readonly Parser<Operation> Addition = Parse.Char(TokenConstants.AdditionOperatorToken).Return(Operation.Addition).Token();
        public static readonly Parser<Operation> Subtraction = Parse.Char(TokenConstants.SubtractionOperatorToken).Return(Operation.Subtraction).Token();
        public static readonly Parser<Operation> Moltiplication = Parse.Char(TokenConstants.MoltiplicationOperatorToken).Return(Operation.Moltiplication).Token();
        public static readonly Parser<Operation> Division = Parse.Char(TokenConstants.DivisionOperatorToken).Return(Operation.Division).Token();
        public static readonly Parser<Operation> Modulo = Parse.String(TokenConstants.ModulopOperatorToken).Return(Operation.Modulo).Token();

        public static readonly Parser<Operation> LeftAssociativeOperators =
            Addition.Or(Subtraction).Or(Moltiplication).Or(Division).Or(Modulo);
        // Right Associative
        public static readonly Parser<Operation> Power = Parse.Char(TokenConstants.PowerOperatorToken).Return(Operation.Power).Token();

        public static readonly Parser<Operation> RightAssociativeOperators =
            Power;

        public static readonly Parser<Operation> All = LeftAssociativeOperators.Or(RightAssociativeOperators);
    }

    internal static class Expressions
    {
        public static readonly Parser<ExpressionSyntax> Expression =
             Parse.Ref<ExpressionSyntax>(() => MemberAccessExpression)
            .Or(Parse.Ref(() => LiteralExpression))
            .Or(Parse.Ref(() => OperationExpression));

        private record LiteralExprHelper(string Literal, LiteralType Type);

        private static Parser<string> SignHelper(Parser<string> parser) =>
            from sign in Parse.Char(TokenConstants.SubtractionOperatorToken).Optional()
            from signed in parser
            select (sign.IsDefined ? "-" : "") + signed;

        public static readonly Parser<LiteralExpressionSyntax> LiteralExpression =
            from helper in SignHelper(Parse.Number).Select(integer => new LiteralExprHelper(integer, LiteralType.Integer))
                        .Or(SignHelper(Parse.Decimal).Select(dec => new LiteralExprHelper(dec, LiteralType.Double)))
            select new LiteralExpressionSyntax(helper.Literal, helper.Type);

        public static readonly Parser<ExpressionSyntax> ChainAssociativeOperation =
            Parse.ChainOperator( // TODO Gets used even for right associative
                Operators.LeftAssociativeOperators,
                Expression,
                (op, left, right) => new OperationExpressionSyntax(left, op, right)
            )
            .Or(Parse.ChainRightOperator(
                Operators.RightAssociativeOperators,
                Expression,
                (op, left, right) => new OperationExpressionSyntax (left, op, right)
            ));

        public static readonly Parser<OperationExpressionSyntax> OperationExpression =
            from leftParent in Parse.Char(TokenConstants.OperationStart).Optional()
            from expression in ChainAssociativeOperation
            from rightParent in Parse.Char(TokenConstants.OperationEnd).Optional()
            select (OperationExpressionSyntax)expression; // ChainAssociativeOperaiton returns only OperationExpressionSyntax

        public static readonly Parser<MemberAccessExpressionSyntax> MemberAccessExpression =
            from identifier in Tokens.Identifier
            from memberAccessOperator in Parse.Char(TokenConstants.MemberAccessOperator)
            from member in Tokens.Identifier
            select new MemberAccessExpressionSyntax(identifier, member);
    }

    internal static class Statements
    {
        public static readonly Parser<ReturnStatement> ReturnStatement =
            from returnToken in Parse.String(TokenConstants.ReturnToken)
            from expression in Expressions.Expression
            select new ReturnStatement(expression);

        public static readonly Parser<StatementSyntax> Statement =
            ReturnStatement;
    }

    // TODO Rivedere la sintassi per il corpo delle azioni / Funzioni
    public static readonly Parser<BodySyntaxDeclaration> BodySyntax =
        from statements in Statements.Statement.Many()
        select new BodySyntaxDeclaration(statements);

    public static readonly Parser<ParameterSyntaxDeclaration> ParameterSyntax =
        from type in Tokens.Type
        from identifier in Tokens.Identifier
        select new ParameterSyntaxDeclaration(type, identifier);

    public static readonly Parser<IEnumerable<ParameterSyntaxDeclaration>> ParametersSyntax =
          from firstParameter in ParameterSyntax
          from otherParameters in (
            from delimiter in Parse.Char(TokenConstants.ParameterDelimiterToken)
            from otherParameter in ParameterSyntax
            select otherParameter
          ).Many()
          select new List<ParameterSyntaxDeclaration> { firstParameter }.Concat(otherParameters);

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
        from identifier in Tokens.Identifier 
        from openBlcok in Parse.Char(TokenConstants.BlockStart)
        from require in RequireSyntax.Optional()
        select new GameActionSyntaxDeclaration(identifier, require.GetOrDefault());
}
