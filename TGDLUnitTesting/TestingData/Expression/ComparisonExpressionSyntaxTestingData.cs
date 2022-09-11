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
                OperationKind.GreaterThan
            )
        },
        new()
        {
            Input = "1 < 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.LessThan
            )
        },
        new()
        {
            Input = "1 >= 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.GreaterOrEqual
            )
        },
        new()
        {
            Input = "1 <= 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.LessOrEqual
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
        new()
        {
            Input = "1>1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.GreaterThan
            )
        },
        new()
        {
            Input = "1 >1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.GreaterThan
            )
        },
        new()
        {
            Input = "1> 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.GreaterThan
            )
        },
        new()
        {
            Input = "1<1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.LessThan
            )
        },
        new()
        {
            Input = "1<=1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.LessOrEqual
            )
        },
        new()
        {
            Input = "1>=1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.GreaterOrEqual
            )
        },
    };
}

