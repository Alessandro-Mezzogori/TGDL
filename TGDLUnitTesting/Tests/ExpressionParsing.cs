using TGDLLib.Syntax;
using TGDLLib.Visitors;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

public class ExpressionParsing
{
    [Theory, ClassData(typeof(LiteralExpressionSyntaxTestingData))]
    public void LiteralExpressionSyntaxTests(DataUnit<string, LiteralExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.literal(), x => new ExpressionVisitor().Visit(x), new ExpressionSyntaxComparer());
    }

    [Theory, ClassData(typeof(ExpressionSyntaxTestingData))]
    public void ExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionVisitor().Visit(x), new ExpressionSyntaxComparer());
    }

    [Theory, ClassData(typeof(BinaryOperationExpressionSyntaxTestingData))]
    public void MathOperationExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionVisitor().Visit(x), new ExpressionSyntaxComparer());
    }

    [Theory, ClassData(typeof(ComparisonBinaryExpressionSyntaxTestingData))]
    public void ComparisonOperationExpressionSyntaxTest(DataUnit<string, BinaryOperationExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionVisitor().Visit(x), new ExpressionSyntaxComparer());
    }

    [Theory, ClassData(typeof(EqualityBinaryOperationExpressionSyntaxTestingData))]
    public void EqualityExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionVisitor().Visit(x), new ExpressionSyntaxComparer());
    }
}
