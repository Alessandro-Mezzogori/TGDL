using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData;

internal class ComparisonExpressionSyntaxTestingData : ParserDataList<ComparisonExpressionSyntax>
{
    public override List<DataUnit<string, ComparisonExpressionSyntax>> DataList => new()
    {
        new()
        {
            Input = "1 > 1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.GreaterThan
            )
        },
        new()
        {
            Input = "1 < 1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.LessThan
            )
        },
        new()
        {
            Input = "1 == 1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.Equal
            )
        },
        new()
        {
            Input = "1 >= 1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.GreaterOrEqual
            )
        },
        new()
        {
            Input = "1 <= 1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.LessOrEqual
            )
        },
        new()
        {
            Input = "1 != 1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.NotEqual
            )
        },
        new()
        {
            Input = "1 + 1 > 1", // TODO comparison operators have lower precedence
            Output = new ComparisonExpressionSyntax(
                sf.Operation(
                    sf.Literal("1", TGDLType.Decimal),
                    sf.Literal("1", TGDLType.Decimal),
                    Operation.Addition
                ),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.GreaterThan
            )
        },
        new()
        {
            Input = "1 == this.attribute",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.AttributeAccess(sf.Identifier("this"), sf.Identifier("attribute")),
                ComparisonOperator.Equal
            )
        },
        new()
        {
            Input = "1>1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.GreaterThan
            )
        },
        new()
        {
            Input = "1 >1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.GreaterThan
            )
        },
        new()
        {
            Input = "1> 1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.GreaterThan
            )
        },
        new()
        {
            Input = "1==1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.Equal
            )
        },
        new()
        {
            Input = "1!=1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.NotEqual
            )
        },
        new()
        {
            Input = "1<1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.LessThan
            )
        },
        new()
        {
            Input = "1<=1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.LessOrEqual
            )
        },
        new()
        {
            Input = "1>=1",
            Output = new ComparisonExpressionSyntax(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                ComparisonOperator.GreaterOrEqual
            )
        },
    };
}

internal class ComparisonExpressionSyntaxComparer : IEqualityComparer<ComparisonExpressionSyntax>
{
    private readonly ExpressionSyntaxComparer _comparer = new();

    public bool Equals(ComparisonExpressionSyntax? x, ComparisonExpressionSyntax? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        return x.Operator == y.Operator
            && _comparer.Equals(x.LeftOperand, y.LeftOperand)
            && _comparer.Equals(x.RightOperand, y.RightOperand);
    }

    public int GetHashCode([DisallowNull] ComparisonExpressionSyntax obj)
    {
        return obj.GetHashCode();
    }
}
