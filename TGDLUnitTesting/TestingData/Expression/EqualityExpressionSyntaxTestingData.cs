using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData;

internal class EqualityBinaryOperationExpressionSyntaxTestingData : ParserDataList<ExpressionSyntax>
{
    public override List<DataUnit<string, ExpressionSyntax>> DataList => new()
    {
        new()
        {
            Input = "1 == 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.Equal
            )
        },
        new()
        {
            Input = "1 != 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.NotEqual
            )
        },
        /*
        new()
        {
            Input = "1 == this.attribute",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.AttributeAccess(sf.Identifier("this"), sf.Identifier("attribute")),
                OperationKind.Equal
            )
        },
        */

        new()
        {
            Input = "1==1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.Equal
            )
        },
        new()
        {
            Input = "1!=1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperationKind.NotEqual
            )
        },
        new()
        {
            Input = "2 > 1==1 < 2",
            Output = sf.BinaryOperation(
                sf.BinaryOperation(
                    sf.Literal("2", TGDLType.Decimal),
                    sf.Literal("1", TGDLType.Decimal),
                    OperationKind.GreaterThan
                ),
                sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal),
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.LessThan
                ),
                OperationKind.Equal
            )
        },
        new()
        {
            Input = "1 + 2 > 1==1 < 2",
            Output = sf.BinaryOperation(
                sf.BinaryOperation(
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("2", TGDLType.Decimal),
                        OperationKind.Addition
                    ),
                    sf.Literal("1", TGDLType.Decimal),
                    OperationKind.GreaterThan
                ),
                sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal),
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.LessThan
                ),
                OperationKind.Equal
            )
        },
    };
}
