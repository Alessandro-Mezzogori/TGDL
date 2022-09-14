using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Expressions;
using TGDLUnitTesting.TestingData.Expression;
using TGDLUnitTesting.TestingData.GenericSyntax;

using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData
{
    internal class ExpressionSyntaxTestingData : ParserDataList<ExpressionSyntax>
    {
        public override List<DataUnit<string, ExpressionSyntax>> DataList => new()
        {
            new()
            {
                Input = "data.access",
                Output = sf.BinaryOperation(
                    sf.IdentifierName("data"),
                    sf.IdentifierName("access"),
                    OperationKind.Dot
                )
            },
            new()
            {
                Input = "access",
                Output = sf.IdentifierName("access")
            },
            new()
            {
                Input = "data.",
                Test = TestType.Fail
            },
            new()
            {
                Input = "1",
                Output = sf.Literal("1", TGDLType.Decimal),
            },
            new()
            {
                Input = "1 + 2",
                Output = sf.BinaryOperation(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal),
                    new LiteralExpressionSyntax("2", TGDLType.Decimal),
                    OperationKind.Addition
                )
            },
            new()
            {
                Input = "1 + 1 > 1", // TODO comparison operators have lower precedence
                Output = sf.BinaryOperation(
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("1", TGDLType.Decimal),
                        OperationKind.Addition
                    ),
                    sf.Literal("1", TGDLType.Decimal),
                    OperationKind.GreaterThan 
                )
            },

        };
    }

    internal class ExpressionSyntaxComparer : IEqualityComparer<ExpressionSyntax>
    {
        private readonly TypeSyntaxComparer _typeComparer = new();
        public bool Equals(ExpressionSyntax? x, ExpressionSyntax? y)
        {
            if(x == null && y == null) return true;
            if(x == null || y == null) return false;
            if (x.GetType() != y.GetType()) return false;

            if(x is LiteralExpressionSyntax)
            {
                var xLiteral = (LiteralExpressionSyntax)x;
                var yLiteral = (LiteralExpressionSyntax)y;

                // TODO Comparer
                return _typeComparer.Equals(xLiteral.Type, yLiteral.Type) 
                    && xLiteral.Value == yLiteral.Value;
            }
            else if(x is IdentifierNameExpressionSyntax)
            {
                var xIdentifier = (IdentifierNameExpressionSyntax)x;
                var yIdentifier = (IdentifierNameExpressionSyntax)y;

                return xIdentifier.Equals(yIdentifier);
            }
            else if(x is BinaryOperationExpressionSyntax)
            {
                var xOperation = (BinaryOperationExpressionSyntax)x;
                var yOperation = (BinaryOperationExpressionSyntax)y;

                return new BinaryOperationExpressionSyntaxComparer().Equals(xOperation, yOperation);
            }
            else if(x is UnaryExpressionSyntax)
            {
                var xUnary = (UnaryExpressionSyntax)x;
                var yUnary = (UnaryExpressionSyntax)y;

                return new UnaryExpressionSyntaxComparer().Equals(xUnary, yUnary);
            }

            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] ExpressionSyntax obj)
        {
            return obj.GetHashCode();
        }
    }


}
