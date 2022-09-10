using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData;

internal class ComparisonBinaryExpressionSyntaxTestingData : ParserDataList<BinaryOperationExpressionSyntax>
{
    public override List<DataUnit<string, BinaryOperationExpressionSyntax>> DataList => new()
    {
        new()
        {
            Input = "1 > 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.GreaterThan
            )
        },
        new()
        {
            Input = "1 < 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.LessThan
            )
        },
        new()
        {
            Input = "1 >= 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.GreaterOrEqual
            )
        },
        new()
        {
            Input = "1 <= 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.LessOrEqual
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
        new()
        {
            Input = "1>1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.GreaterThan
            )
        },
        new()
        {
            Input = "1 >1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.GreaterThan
            )
        },
        new()
        {
            Input = "1> 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.GreaterThan
            )
        },
        new()
        {
            Input = "1<1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.LessThan
            )
        },
        new()
        {
            Input = "1<=1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.LessOrEqual
            )
        },
        new()
        {
            Input = "1>=1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.GreaterOrEqual
            )
        },
    };
}

