using Sprache;
using TGDLLib.Syntax;

namespace TGDLLib;

internal static class Grammar
{
    internal static class Tokens
    {
        private static readonly Parser<string> Token =
            Parse.Letter.AtLeastOnce().Concat(Parse.LetterOrDigit.Many()).Text().Token();

        // Action Parsers
        public static readonly Parser<IdentifierSyntaxToken> Identifier =
            from token in Token select new IdentifierSyntaxToken(token);

        public static readonly Parser<TypeSyntaxToken> Type =
            from token in Token select new TypeSyntaxToken(token);
    }

    internal static class Operators
    {
        // Left Associative 
        public static readonly Parser<Operation> Addition = Parse.Char(TokenConstants.AdditionOperatorToken).Return(Operation.Addition).Token();
        public static readonly Parser<Operation> Subtraction = Parse.Char(TokenConstants.SubtractionOperatorToken).Return(Operation.Subtraction).Token();
        public static readonly Parser<Operation> Moltiplication = Parse.Char(TokenConstants.MoltiplicationOperatorToken).Return(Operation.Moltiplication).Token();
        public static readonly Parser<Operation> Division = Parse.Char(TokenConstants.DivisionOperatorToken).Return(Operation.Division).Token();
        public static readonly Parser<Operation> Modulo = Parse.String(TokenConstants.ModulopOperatorToken).Return(Operation.Modulo).Token();

        // Right Associative
        public static readonly Parser<Operation> Power = Parse.Char(TokenConstants.PowerOperatorToken).Return(Operation.Power).Token();
    }

    internal static class Expressions
    {
        public static readonly Parser<ExpressionSyntax> Expression =
            Parse.Ref<ExpressionSyntax>(() => OperationExpression)
            .XOr(Parse.Ref(() => MemberAccessExpression))
            .XOr(Parse.Ref(() => LiteralExpression));
        

        // TODO Redo all the parsers
        public static readonly Parser<ExpressionSyntax> ExpressionReverse = 
            Parse.Ref<ExpressionSyntax>(() => LiteralExpression)
            .XOr(Parse.Ref(() => MemberAccessExpression))
            .XOr(Parse.Ref(() => OperationExpression));

        private record LiteralExprHelper(string Literal, LiteralType Type);
        private static Parser<string> SignHelper(Parser<string> parser) =>
            from sign in Parse.Char(TokenConstants.SubtractionOperatorToken).Optional()
            from signed in parser
            select (sign.IsDefined ? "-" : "") + signed;

        public static readonly Parser<LiteralExpressionSyntax> LiteralExpression =
            from helper in SignHelper(Parse.Number).Select(integer => new LiteralExprHelper(integer, LiteralType.Integer))
                        .Or(SignHelper(Parse.Decimal).Select(dec => new LiteralExprHelper(dec, LiteralType.Double)))
            select new LiteralExpressionSyntax(helper.Literal, helper.Type);

        public static readonly Parser<ExpressionSyntax> InnerOperation =
            Parse.ChainRightOperator(
                Operators.Power,
                ExpressionReverse,
                (op, left, right) => new OperationExpressionSyntax(left, op, right));

        public static readonly Parser<ExpressionSyntax> Factor =
            Parse.ChainOperator(
                Operators.Moltiplication.Or(Operators.Division).Or(Operators.Modulo),
                InnerOperation,
                (op, left, right) => new OperationExpressionSyntax(left, op, right));

        public static readonly Parser<ExpressionSyntax> Term =
            Parse.ChainOperator(
                Operators.Addition.Or(Operators.Subtraction),
                Factor,
                (op, left, right) => new OperationExpressionSyntax(left, op, right));

        public static readonly Parser<ExpressionSyntax> OperationExpression =
            from leftParent in Parse.Char(TokenConstants.OperationStart).Optional()
            from expression in Term
            from rightParent in Parse.Char(TokenConstants.OperationEnd).Optional()
            select expression;

        public static readonly Parser<MemberAccessExpressionSyntax> MemberAccessExpression =
            from identifier in (
                from id in Tokens.Identifier
                from memberAccessOperator in Parse.Char(TokenConstants.MemberAccessOperator)
                select id
            ).Optional()
            from member in Tokens.Identifier
            select new MemberAccessExpressionSyntax(identifier.GetOrDefault() ?? new IdentifierSyntaxToken(TokenConstants.ThisToken), member);
    }

    internal static class Statements
    {
        public static readonly Parser<ReturnStatementSyntax> ReturnStatement =
            from returnToken in Parse.String(TokenConstants.ReturnToken)
            from whitespace in Parse.WhiteSpace.AtLeastOnce()
            from expression in Expressions.Expression
            select new ReturnStatementSyntax(expression);

        // Assignment
        // Invocation

        public static readonly Parser<StatementSyntax> Statement =
            ReturnStatement;
    }


    // TODO Rivedere la sintassi per il corpo delle azioni / Funzioni
    public static readonly Parser<BodySyntaxDeclaration> BodySyntax =
        from body in 
            from openBlock in Parse.String(TokenConstants.LambdaBodyDelimiter)
            from allowedWhitespace in Parse.WhiteSpace.Many()
            from statements in (
                from statement in Statements.Statement
                from endOfStatement in Parse.LineEnd.Or(Parse.LineTerminator)
                select statement
            ).Many()
            select new BodySyntaxDeclaration(statements)
        select body;


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

    public static readonly Parser<RequireLambdaSyntaxDeclaration> RequireLambdaExpression =
        from parameters in ParametersSyntax.Optional()
        from delimiter in Parse.String(TokenConstants.LambdaBodyDelimiter)
        from openBlock in Parse.Char(TokenConstants.BlockStart)
        from body in BodySyntax
        select new RequireLambdaSyntaxDeclaration(parameters.GetOrElse(Enumerable.Empty<ParameterSyntaxDeclaration>()), body);

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
