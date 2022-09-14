using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks.Sources;
using TGDLLib;
using TGDLLib.Syntax;

namespace TGDLUnitTesting;

public class TokenParsing
{
    [Fact]
    public void TestTokenParsing()
    {
    }
}

public class ExpressionTester : TGDLBaseVisitor<ExpressionSyntax> 
{
    public override ExpressionSyntax VisitIdentifier([NotNull] TGDLParser.IdentifierContext context)
    {
        var identifier = context.IDENTIFIER();
        if (identifier != null)
            return SyntaxFactory.IdentifierName(identifier.GetText());

        throw new NotImplementedException("Identifier");
    }

    public override ExpressionSyntax VisitLiteral([NotNull] TGDLParser.LiteralContext context)
    {
        var dec = context.DECIMAL();
        if (dec != null)
            return SyntaxFactory.Literal(dec.GetText(), TGDLType.Decimal);

        var str = context.STRING();
        if (str != null)
            return SyntaxFactory.Literal(str.GetText(), TGDLType.String);

        var @bool = context.BOOL();
        if (@bool != null)
            return SyntaxFactory.Literal(@bool.GetText(), TGDLType.Bool);

        throw new NotImplementedException("Literal");
    }

    public override ExpressionSyntax VisitPrimary([NotNull] TGDLParser.PrimaryContext context)
    {
        var literal = context.literal();
        if (literal != null)
            return VisitLiteral(literal);

        var identifer = context.identifier();
        if (identifer != null)
            return VisitIdentifier(identifer);

        var expression = context.expression();
        if (expression != null)
            return VisitExpression(expression);

        throw new NotImplementedException("Primary");
    }

    public override ExpressionSyntax VisitExpression([NotNull] TGDLParser.ExpressionContext context)
    {
        var primary = context.primary();
        if (primary != null)
            return VisitPrimary(primary);

        if(context.dot != null)
        {
            var expr = VisitExpression(context.expression(0));
            var identifier = VisitIdentifier(context.identifier());

            return SyntaxFactory.BinaryOperation(expr, identifier, OperationKind.Dot);
        }

        if(context.prefix != null)
        {
            var op = context.prefix.Type switch
            {
                TGDLLexer.NOT   => OperationKind.Not,
                TGDLLexer.PLUS  => OperationKind.Plus,
                TGDLLexer.MINUS => OperationKind.Minus,
                _   => throw new NotImplementedException("prefix operator not found") 
            };

            var operand = VisitExpression(context.expression(0));

            return SyntaxFactory.Unary(operand, op);
        }

        if(context.bop != null)
        {
            OperationKind op = context.bop.Type switch
            {
                TGDLLexer.POW           => OperationKind.Power,
                TGDLLexer.MOL           => OperationKind.Moltiplication,
                TGDLLexer.DIV           => OperationKind.Division,
                TGDLLexer.MOD           => OperationKind.Modulo,
                TGDLLexer.PLUS          => OperationKind.Addition,
                TGDLLexer.MINUS         => OperationKind.Subtraction,
                TGDLLexer.LESSEQUAL     => OperationKind.LessOrEqual,
                TGDLLexer.GREATEREQUAL  => OperationKind.GreaterOrEqual,
                TGDLLexer.LESS          => OperationKind.LessThan,
                TGDLLexer.GREATER       => OperationKind.GreaterThan,
                TGDLLexer.EQUAL         => OperationKind.Equal,
                TGDLLexer.NOTEQUAL      => OperationKind.NotEqual,
                TGDLLexer.AND           => OperationKind.And,
                TGDLLexer.OR            => OperationKind.Or, 
                _    => throw new NotImplementedException($"no operator found: {context.bop.Text} {context.bop.TokenIndex}")
            };

            var left = VisitExpression(context.expression(0));
            var right = VisitExpression(context.expression(1));

            return SyntaxFactory.BinaryOperation(left, right, op);
        }

        throw new NotImplementedException("Expression");
    }
}

