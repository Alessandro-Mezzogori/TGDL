using Sprache;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Expressions;

namespace TGDLLib;

internal static class Grammar
{
    internal static class Tokens
    {
        private static readonly Parser<string> Token =
            Parse.Letter.AtLeastOnce().Concat(Parse.LetterOrDigit.Many()).Text().Token();

        public static readonly Parser<IdentifierToken> Identifier =
            from token in Token select SyntaxFactory.Identifier(token);

        public static readonly Parser<TypeSyntax> Type =
            from token in Token select SyntaxFactory.ParseTypeName(token);

        public static readonly Parser<StateScopeToken> StateScope =
            Parse.String(TokenConstants.LocalToken).Return(Syntax.StateScope.Local)
            .Or(Parse.String(TokenConstants.GroupToken).Return(Syntax.StateScope.Group))
            .Or(Parse.String(TokenConstants.GlobalToken).Return(Syntax.StateScope.Global))
            .Select(x => SyntaxFactory.StateScope(x));
    }

    internal static class Operators
    {
        // Left Associative 
        public static readonly Parser<OperationKind> Addition = Parse.Char(TokenConstants.AdditionOperatorToken).Return(OperationKind.Addition).Token();
        public static readonly Parser<OperationKind> Subtraction = Parse.Char(TokenConstants.SubtractionOperatorToken).Return(OperationKind.Subtraction).Token();
        public static readonly Parser<OperationKind> Moltiplication = Parse.Char(TokenConstants.MoltiplicationOperatorToken).Return(OperationKind.Moltiplication).Token();
        public static readonly Parser<OperationKind> Division = Parse.Char(TokenConstants.DivisionOperatorToken).Return(OperationKind.Division).Token();
        public static readonly Parser<OperationKind> Modulo = Parse.String(TokenConstants.ModulopOperatorToken).Return(OperationKind.Modulo).Token();

        // Right Associative
        public static readonly Parser<OperationKind> Power = Parse.Char(TokenConstants.PowerOperatorToken).Return(OperationKind.Power).Token();

        // Comparison operators all are left associative

        public static readonly Parser<OperationKind> Equal = Parse.String(TokenConstants.EqualOperator).Return(OperationKind.Equal).Token();
        public static readonly Parser<OperationKind> NotEquals = Parse.String(TokenConstants.NotEqualOperator).Return(OperationKind.NotEqual).Token();
        public static readonly Parser<OperationKind> GreaterThan = Parse.Char(TokenConstants.GreaterThanOperator).Return(OperationKind.GreaterThan).Token();
        public static readonly Parser<OperationKind> GreaterOrEqual = Parse.String(TokenConstants.GreaterOrEqualOperator).Return(OperationKind.GreaterOrEqual).Token();
        public static readonly Parser<OperationKind> LessThan = Parse.Char(TokenConstants.LessThanOperator).Return(OperationKind.LessThan).Token();
        public static readonly Parser<OperationKind> LessOrEqual = Parse.String(TokenConstants.LessOrEqualOperato).Return(OperationKind.LessOrEqual).Token();

        public static readonly Parser<OperationKind> AttributeAccess = Parse.Chars(TokenConstants.AttributeAccessOperator).Return(OperationKind.AttributeAccess).Token();
    }

    internal static class Literals
    {
        public static readonly Parser<char> Sign = Parse.Char(TokenConstants.SubtractionOperatorToken);

        public static readonly Parser<LiteralExpressionSyntax> Decimal = 
            from sign in Sign.Optional()
            from dec in Parse.DecimalInvariant
            select new LiteralExpressionSyntax(sign.IsDefined ? sign.Get() + dec : dec,TGDLType.Decimal); 

        public static readonly Parser<LiteralExpressionSyntax> String = 
            from openQuote in Parse.Char('"')
            from content in Parse.AnyChar.Until(Parse.Char('"')).Text()
            select new LiteralExpressionSyntax(content, TGDLType.String);

        public static readonly Parser<LiteralExpressionSyntax> Bool = 
            from boolean in Parse.String(TokenConstants.True).XOr(Parse.String(TokenConstants.False)).Text()
            from not in Parse.Not(Parse.LetterOrDigit.AtLeastOnce())
            select  new LiteralExpressionSyntax(boolean, TGDLType.Bool); 
    }

    internal static class Expressions
    {
        public static readonly Parser<ExpressionSyntax> ValidExpression =
            Parse.Ref<ExpressionSyntax>(() => LiteralExpression)
            .Or(Parse.Ref(() => IdentifierName))
            .XOr(Parse.Ref(() => Expression).InParenthesis()) // Through recursion all inner expressions will have the optional parenthesis
            .Named("BinaryOperationInnerExpressions");
        
        public static readonly Parser<LiteralExpressionSyntax> LiteralExpression =
            Literals.Decimal
            .Or(Literals.String)
            .Or(Literals.Bool)
            .Named("Literal");

        public static readonly Parser<IdentifierNameExpressionSyntax> IdentifierName = 
            Tokens.Identifier
            .Select(x => SyntaxFactory.IdentifierName(x));

        public static readonly Parser<ExpressionSyntax> AccessOperand =
            Parse.ChainOperator(
                Operators.AttributeAccess,
                ValidExpression,
                (op, left, right) => SyntaxFactory.BinaryOperation(left, right, op)
            ).Named("AccessOperand");

        public static readonly Parser<ExpressionSyntax> PowerTerm =
            Parse.ChainRightOperator(
                Operators.Power,
                AccessOperand,
                (op, left, right) => SyntaxFactory.BinaryOperation(left, right, op)
            ).Named("PowerTerm");

        public static readonly Parser<ExpressionSyntax> Factor =
            Parse.ChainOperator(
                Operators.Moltiplication.Or(Operators.Division).Or(Operators.Modulo),
                PowerTerm,
                (op, left, right) => SyntaxFactory.BinaryOperation(left, right, op)
            ).Named("Factor");

        public static readonly Parser<ExpressionSyntax> AddSubTerm =
            Parse.ChainOperator(
                Operators.Addition.Or(Operators.Subtraction),
                Factor,
                (op, left, right) => SyntaxFactory.BinaryOperation(left, right, op)
            ).Named("AddSubTerm");

        public static readonly Parser<ExpressionSyntax> ComparisonTerm =
            Parse.ChainOperator(
                Operators.GreaterOrEqual.Or(Operators.GreaterThan).Or(Operators.LessOrEqual).Or(Operators.LessThan),
                AddSubTerm,
                (op, left, right) => SyntaxFactory.BinaryOperation(left, right, op)
            ).Named("ComparisonTerm");
            
        public static readonly Parser<ExpressionSyntax> EqualityTerm =
            Parse.ChainOperator(
                Operators.Equal.Or(Operators.NotEquals),
                ComparisonTerm,
                (op, left, right) => SyntaxFactory.BinaryOperation(left, right, op)
            ).Named("EqualityTerm");

        public static readonly Parser<ExpressionSyntax> Expression = EqualityTerm.Named("Expression");
    }

    internal static class Statements
    {
        public static readonly Parser<ReturnStatementSyntax> ReturnStatement =
            from returnToken in Parse.String(TokenConstants.ReturnToken)
            from whitespace in Parse.WhiteSpace.AtLeastOnce()
            from expression in Expressions.Expression
            select new ReturnStatementSyntax(expression);

        // Invocation

        public static readonly Parser<StatementSyntax> Statement =
            ReturnStatement;
    }


    // TODO Rivedere la sintassi per il corpo delle azioni / Funzioni
    public static readonly Parser<BodySyntaxDeclaration> BodySyntax =
        from openToken in Parse.String(TokenConstants.LambdaBodyDelimiter)
        from openWhitespaces in Parse.WhiteSpace.Many()
        from statements in (
            from statement in Statements.Statement
            from statementEnd in Parse.LineTerminator
            select statement
        ).AtLeastOnce()
        .XOr(
            from noStatements in Parse.Not(Parse.WhiteSpace.Many().Then(x => Parse.LineTerminator))
            from expression in Expressions.Expression
            from expressionEnd in Parse.LineTerminator
            select new[] { SyntaxFactory.Return(expression)}
        )
        select SyntaxFactory.Body(statements);

    public static readonly Parser<ParameterSyntaxDeclaration> ParameterSyntax =
        from type in Tokens.Type
        from identifier in Tokens.Identifier
        select new ParameterSyntaxDeclaration(type, identifier);

    public static readonly Parser<IEnumerable<ParameterSyntaxDeclaration>> ParametersSyntax =
          from firstParameter in ParameterSyntax
          from otherParameters in (
            from delimiter in Parse.Char(TokenConstants.ParameterDelimiterToken).Token()
            from otherParameter in ParameterSyntax
            select otherParameter
          ).Many()
          select new List<ParameterSyntaxDeclaration> { firstParameter }.Concat(otherParameters);

    public static readonly Parser<LambdaSyntaxDeclaration> Lambda =
        from parameters in ParametersSyntax.Token().Optional()
        from body in BodySyntax
        select SyntaxFactory.Lambda(body, parameters.GetOrDefault());

    public static readonly Parser<AttributeSyntaxDeclaration> Attribute =
        from identifier in Tokens.Identifier
        from assignmentOperator in Parse.Char(TokenConstants.AssignmentOperator).Token()
        from initializer in Expressions.LiteralExpression
        select SyntaxFactory.StateAttribute(identifier, initializer);

    public static readonly Parser<StateSyntaxDeclaration> State =
        from scope in (   
            from scope in Tokens.StateScope
            from scopeWhitespace in Parse.WhiteSpace.AtLeastOnce()
            select scope
        ).Optional()
        from stateKeyword in Parse.String(TokenConstants.StateToken)
        from stateWhitespace in Parse.WhiteSpace.AtLeastOnce()
        from identifier in Tokens.Identifier
        from attributes in
        (
            from openStateToken in Parse.Char(TokenConstants.BlockStart)
            from newLine in Parse.LineEnd
            from attributes in (
                from attribute in Attribute
                from endLine in Parse.LineTerminator
                select attribute
            ).Token().Many()
            select attributes
        ).Optional()
            // action definitions
        select SyntaxFactory.State(identifier, scope.GetOrDefault(), attributes.GetOrDefault());


    public static readonly Parser<RequireSyntaxDeclaration> Require =
        from requireToken in Parse.String(TokenConstants.RequireToken)
        from openRequire in Parse.Char(TokenConstants.BlockStart)
        from openNewLine in Parse.LineEnd
        from requirements in Lambda.AtLeastOnce()
        select new RequireSyntaxDeclaration(requirements);
}

public static class ParserExtensions
{
    public static Parser<T> InParenthesis<T>(this Parser<T> parser)
    {
        return
            (
                from leftParenthesis in Parse.Char(TokenConstants.OperationStart).Token()
                from value in parser
                from rightParenthesis in Parse.Char(TokenConstants.OperationEnd).Token()
                select value
            )
            .Or(parser);
    }

}
