using Sprache;
using System.Security.Cryptography;
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
        public static readonly Parser<Operation> Addition = Parse.Char(TokenConstants.AdditionOperatorToken).Return(Operation.Addition).Token();
        public static readonly Parser<Operation> Subtraction = Parse.Char(TokenConstants.SubtractionOperatorToken).Return(Operation.Subtraction).Token();
        public static readonly Parser<Operation> Moltiplication = Parse.Char(TokenConstants.MoltiplicationOperatorToken).Return(Operation.Moltiplication).Token();
        public static readonly Parser<Operation> Division = Parse.Char(TokenConstants.DivisionOperatorToken).Return(Operation.Division).Token();
        public static readonly Parser<Operation> Modulo = Parse.String(TokenConstants.ModulopOperatorToken).Return(Operation.Modulo).Token();

        // Right Associative
        public static readonly Parser<Operation> Power = Parse.Char(TokenConstants.PowerOperatorToken).Return(Operation.Power).Token();

        // Comparison operators

        public static readonly Parser<ComparisonOperator> Equal = Parse.String(TokenConstants.EqualOperator).Return(ComparisonOperator.Equal).Token();
        public static readonly Parser<ComparisonOperator> NotEqual = Parse.String(TokenConstants.NotEqualOperator).Return(ComparisonOperator.NotEqual).Token();
        public static readonly Parser<ComparisonOperator> GreaterThan = Parse.Char(TokenConstants.GreaterThanOperator).Return(ComparisonOperator.GreaterThan).Token();
        public static readonly Parser<ComparisonOperator> GreaterOrEqual = Parse.String(TokenConstants.GreaterOrEqualOperator).Return(ComparisonOperator.GreaterOrEqual).Token();
        public static readonly Parser<ComparisonOperator> LessThan = Parse.Char(TokenConstants.LessThanOperator).Return(ComparisonOperator.LessThan).Token();
        public static readonly Parser<ComparisonOperator> LessOrEqual = Parse.String(TokenConstants.LessOrEqualOperato).Return(ComparisonOperator.LessOrEqual).Token();

        public static readonly Parser<ComparisonOperator> ComparisonOperators = 
                Equal
                .Or(NotEqual)
                .Or(GreaterOrEqual)
                .Or(GreaterThan)
                .Or(LessOrEqual)
                .Or(LessThan);
    }

    internal static class Literals
    {
        public static readonly Parser<char> Sign = Parse.Char(TokenConstants.SubtractionOperatorToken);

        public static readonly Parser<LiteralExpressionSyntax> Decimal = 
            from sign in Sign.Optional()
            from dec in Parse.DecimalInvariant
            select new LiteralExpressionSyntax(sign.IsDefined ? sign.Get() + dec : dec,TGDLType.Decimal); // TODO check if decimal includes int or only doubles

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
        public static readonly Parser<ExpressionSyntax> Expression =
            Parse.Ref<ExpressionSyntax>(() => ComparisonExpression)
            .Or(Parse.Ref<ExpressionSyntax>(() => OperationExpression))
            .XOr(Parse.Ref(() => MemberAccessExpression))
            .XOr(Parse.Ref(() => LiteralExpression))
            ;


        // TODO Redo all the parsers
        public static readonly Parser<ExpressionSyntax> ExpressionReverse =
            Parse.Ref<ExpressionSyntax>(() => LiteralExpression)
            .Or(Parse.Ref(() => MemberAccessExpression))
            .Or(Parse.Ref(() => OperationExpression))
            .XOr(Parse.Ref(() => ComparisonExpression))
            ;


        public static readonly Parser<LiteralExpressionSyntax> LiteralExpression =
            Literals.Decimal
            .Or(Literals.String)
            .Or(Literals.Bool);
            
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

        public static readonly Parser<AttributeAccessExpressionSyntax> MemberAccessExpression =
            from identifier in (
                from id in Tokens.Identifier
                from memberAccessOperator in Parse.Char(TokenConstants.MemberAccessOperator)
                select id
            ).Optional()
            from member in Tokens.Identifier
            select new AttributeAccessExpressionSyntax(identifier.GetOrElse(new IdentifierSyntaxToken(TokenConstants.ThisToken)), member);

        public static readonly Parser<ComparisonExpressionSyntax> ComparisonExpression =
            from leftOperand in ExpressionReverse
            from comparisonOperator in Operators.ComparisonOperators
            from rightOperand in ExpressionReverse 
            select SyntaxFactory.Comparison(leftOperand, rightOperand, comparisonOperator);
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

