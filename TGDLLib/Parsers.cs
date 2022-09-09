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

        public static readonly Parser<TypeSyntax> Type =
            from token in Token select SyntaxFactory.ParseTypeName(token);

        public static readonly Parser<string> StateScope =
            Parse.String(TokenConstants.LocalToken).Text()
            .XOr(Parse.String(TokenConstants.GroupToken).Text())
            .XOr(Parse.String(TokenConstants.GlobalToken).Text());
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
            Parse.Ref<ExpressionSyntax>(() => OperationExpression)
            .XOr(Parse.Ref(() => MemberAccessExpression))
            .XOr(Parse.Ref(() => LiteralExpression));
        

        // TODO Redo all the parsers
        public static readonly Parser<ExpressionSyntax> ExpressionReverse = 
            Parse.Ref<ExpressionSyntax>(() => LiteralExpression)
            .Or(Parse.Ref(() => MemberAccessExpression))
            .XOr(Parse.Ref(() => OperationExpression));



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

    public static readonly Parser<LambdaSyntaxDeclaration> LambdaExpression =
        from parameters in ParametersSyntax.Optional()
        from delimiter in Parse.String(TokenConstants.LambdaBodyDelimiter)
        from openBlock in Parse.Char(TokenConstants.BlockStart)
        from body in BodySyntax
        select new LambdaSyntaxDeclaration(parameters.GetOrElse(Enumerable.Empty<ParameterSyntaxDeclaration>()), body);

/*
    public static readonly Parse<AttributeSyntaxDeclaration> AttributeDeclaration =
        from identifier in Tokens.Identifier
        from assignmentOperator in Parse.Char(TokenConstants.AssignmentOperator)
        from defaultValue in Parse.AnyChar.AtLeastOnce().Until(Parse.LineEnd)
        select 

    public static readonly Parser<StateSyntaxDeclaration> State =
        from scope in Tokens.StateScope.Optional()
        from stateKeyword in Parse.String(TokenConstants.StateToken)
        from identifier in Tokens.Identifier
        from openStateToken in Parse.Char(TokenConstants.BlockStart)
        from newLine in Parse.LineEnd
            // attribute definitions
            // action definitions
        select new StateSyntaxDeclaration()



    public static readonly Parser<RequireSyntaxDeclaration> RequireSyntax =
        from requireToken in Parse.String(TokenConstants.RequireToken)
        from lambdas in LambdaExpression.AtLeastOnce()
        select new RequireSyntaxDeclaration(lambdas);

    public static readonly Parser<GameActionSyntaxDeclaration> GameActionSyntaxDeclaration =
        from actionToken in Parse.String(TokenConstants.ActionToken)
        from identifier in Tokens.Identifier
        from openBlcok in Parse.Char(TokenConstants.BlockStart)
        from require in RequireSyntax.Optional()
        select new GameActionSyntaxDeclaration(identifier, require.GetOrDefault());
*/
}


public static class ParseExtensions
{
    public static Parser<string> Alone(string pattern)
    {
        var expectations = new string[] { pattern };
        
        return delegate (IInput i) 
        {
             if (!i.AtEnd)
            {
                IInput input = i;
                string text = i.Source.Substring(i.Position);

                var result = text.StartsWith(pattern);
                if (result)
                {
                    var charAfterPattern = text.Substring(pattern.Length).FirstOrDefault();
                    if(charAfterPattern == default || char.IsWhiteSpace(charAfterPattern))
                        return Result.Success(pattern, input);
                }

                return Result.Failure<string>(input, $"expected {pattern}", expectations);
            }

            return Result.Failure<string>(i, "Unexpected end of input", expectations);
        };
    }
/*
    public static Parser<string> StringNotConsuming(string s)
    {
        if(s == null)
        {
            throw new ArgumentNullException(nameof(s));
        }

        return s.ToEnumerable()
            .Select(new Func<char, Parser<char>>(Char))
            .Aggregate(
                Return(Enumerable.Empty<char>()), 
                (Parser<IEnumerable<char>> a, Parser<char> p) => a.Concat(p.Once()))
                .Named(s);

            
    }*/
}
