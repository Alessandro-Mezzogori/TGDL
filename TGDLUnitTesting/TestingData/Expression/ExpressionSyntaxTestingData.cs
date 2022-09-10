using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
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
                Output = sf.AttributeAccess(new("data"), new("access"))
            },
            new()
            {
                Input = "access",
                Output = sf.AttributeAccess(new("this"), new("access"))
            },
            new()
            {
                Input = "data.",
                Output = new(),
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
                    OperatorKind.Addition
                )
            },
            new()
            {
                Input = "1 + 1 > 1", // TODO comparison operators have lower precedence
                Output = sf.BinaryOperation(
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("1", TGDLType.Decimal),
                        OperatorKind.Addition
                    ),
                    sf.Literal("1", TGDLType.Decimal),
                    OperatorKind.GreaterThan 
                )
            },

        };

        public void test()
        {
            bool test = 1 + 1 > 2;
        }
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
            else if(x is AttributeAccessExpressionSyntax)
            {
                var xAccess = (AttributeAccessExpressionSyntax)x;
                var yAccess = (AttributeAccessExpressionSyntax)y;

                return new MemberAccessExpressionSyntaxComparer().Equals(xAccess, yAccess);
            }
            else if(x is BinaryOperationExpressionSyntax)
            {
                var xOperation = (BinaryOperationExpressionSyntax)x;
                var yOperation = (BinaryOperationExpressionSyntax)y;

                return new BinaryOperationExpressionSyntaxComparer().Equals(xOperation, yOperation);
            }

            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] ExpressionSyntax obj)
        {
            return obj.GetHashCode();
        }
    }


}
