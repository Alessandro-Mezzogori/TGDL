﻿using Sprache;
using System.Runtime.CompilerServices;
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
        public static readonly Parser<OperatorKind> Addition = Parse.Char(TokenConstants.AdditionOperatorToken).Return(OperatorKind.Addition).Token();
        public static readonly Parser<OperatorKind> Subtraction = Parse.Char(TokenConstants.SubtractionOperatorToken).Return(OperatorKind.Subtraction).Token();
        public static readonly Parser<OperatorKind> Moltiplication = Parse.Char(TokenConstants.MoltiplicationOperatorToken).Return(OperatorKind.Moltiplication).Token();
        public static readonly Parser<OperatorKind> Division = Parse.Char(TokenConstants.DivisionOperatorToken).Return(OperatorKind.Division).Token();
        public static readonly Parser<OperatorKind> Modulo = Parse.String(TokenConstants.ModulopOperatorToken).Return(OperatorKind.Modulo).Token();

        // Right Associative
        public static readonly Parser<OperatorKind> Power = Parse.Char(TokenConstants.PowerOperatorToken).Return(OperatorKind.Power).Token();

        // Comparison operators all are left associative

        public static readonly Parser<OperatorKind> Equal = Parse.String(TokenConstants.EqualOperator).Return(OperatorKind.Equal).Token();
        public static readonly Parser<OperatorKind> NotEquals = Parse.String(TokenConstants.NotEqualOperator).Return(OperatorKind.NotEqual).Token();
        public static readonly Parser<OperatorKind> GreaterThan = Parse.Char(TokenConstants.GreaterThanOperator).Return(OperatorKind.GreaterThan).Token();
        public static readonly Parser<OperatorKind> GreaterOrEqual = Parse.String(TokenConstants.GreaterOrEqualOperator).Return(OperatorKind.GreaterOrEqual).Token();
        public static readonly Parser<OperatorKind> LessThan = Parse.Char(TokenConstants.LessThanOperator).Return(OperatorKind.LessThan).Token();
        public static readonly Parser<OperatorKind> LessOrEqual = Parse.String(TokenConstants.LessOrEqualOperato).Return(OperatorKind.LessOrEqual).Token();
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
        public static readonly Parser<LiteralExpressionSyntax> LiteralExpression =
            Literals.Decimal
            .Or(Literals.String)
            .Or(Literals.Bool)
            .Named("Literal");

        public static readonly Parser<AttributeAccessExpressionSyntax> AttributeAccessExpression =
            from identifier in (
                from id in Tokens.Identifier
                from memberAccessOperator in Parse.Char(TokenConstants.MemberAccessOperator)
                select id
            ).Optional()
            from member in Tokens.Identifier
            select new AttributeAccessExpressionSyntax(identifier.GetOrElse(new IdentifierSyntaxToken(TokenConstants.ThisToken)), member);

        public static readonly Parser<ExpressionSyntax> ValidExpressions =
            LiteralExpression
            .Or<ExpressionSyntax>(AttributeAccessExpression)
            .XOr(Parse.Ref(() => Expression).InParenthesis()) // Through recursion all inner expressions will have the optional parenthesis
            .Named("BinaryOperationInnerExpressions");
            
        public static readonly Parser<ExpressionSyntax> PowerTerm =
            Parse.ChainRightOperator(
                Operators.Power,
                ValidExpressions,
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
