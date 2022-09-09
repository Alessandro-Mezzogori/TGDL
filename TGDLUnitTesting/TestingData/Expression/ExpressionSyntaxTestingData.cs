using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData.GenericSyntax;

namespace TGDLUnitTesting.TestingData
{
    internal class ExpressionSyntaxTestingData : ParserDataList<ExpressionSyntax>
    {
        public override List<DataUnit<string, ExpressionSyntax>> DataList => new()
        {
            new()
            {
                Input = "data.access",
                Output = new AttributeAccessExpressionSyntax(new("data"), new("access"))
            },
            new()
            {
                Input = "access",
                Output = new AttributeAccessExpressionSyntax(new("this"), new("access"))
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
                Output = new LiteralExpressionSyntax("1", TGDLType.Decimal),
            },
            new()
            {
                Input = "1 + 2",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal),
                    Operation.Addition,
                    new LiteralExpressionSyntax("2", TGDLType.Decimal)
                )
            }
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
            else if(x is AttributeAccessExpressionSyntax)
            {
                var xAccess = (AttributeAccessExpressionSyntax)x;
                var yAccess = (AttributeAccessExpressionSyntax)y;

                return new MemberAccessExpressionSyntaxComparer().Equals(xAccess, yAccess);
            }
            else if(x is OperationExpressionSyntax)
            {
                var xOperation = (OperationExpressionSyntax)x;
                var yOperation = (OperationExpressionSyntax)y;

                return new OperationExpressionSyntaxComparer().Equals(xOperation, yOperation);
            }

            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] ExpressionSyntax obj)
        {
            return obj.GetHashCode();
        }
    }


}
