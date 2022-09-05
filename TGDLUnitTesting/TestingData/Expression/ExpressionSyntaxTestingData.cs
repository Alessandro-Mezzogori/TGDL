using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class ExpressionSyntaxTestingData : ParserDataList<ExpressionSyntax>
    {
        public override List<DataUnit<string, ExpressionSyntax>> DataList => new()
        {
            new()
            {
                Input = "data.access",
                Output = new MemberAccessExpressionSyntax(new("data"), new("access"))
            }
        };
    }

    internal class ExpressionSyntaxComparer : IEqualityComparer<ExpressionSyntax>
    {
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
                return xLiteral.Type == yLiteral.Type && xLiteral.Value == yLiteral.Value;
            }
            else if(x is MemberAccessExpressionSyntax)
            {
                var xAccess = (MemberAccessExpressionSyntax)x;
                var yAccess = (MemberAccessExpressionSyntax)y;

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
