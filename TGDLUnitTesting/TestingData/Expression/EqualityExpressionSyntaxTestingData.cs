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
                OperatorKind.Equal
            )
        },
        new()
        {
            Input = "1 != 1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.NotEqual
            )
        },
        new()
        {
            Input = "1 == this.attribute",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.AttributeAccess(sf.Identifier("this"), sf.Identifier("attribute")),
                OperatorKind.Equal
            )
        },

        new()
        {
            Input = "1==1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.Equal
            )
        },
        new()
        {
            Input = "1!=1",
            Output = sf.BinaryOperation(
                sf.Literal("1", TGDLType.Decimal),
                sf.Literal("1", TGDLType.Decimal),
                OperatorKind.NotEqual
            )
        },
        new()
        {
            Input = "2 > 1==1 < 2",
            Output = sf.BinaryOperation(
                sf.BinaryOperation(
                    sf.Literal("2", TGDLType.Decimal),
                    sf.Literal("1", TGDLType.Decimal),
                    OperatorKind.GreaterThan
                ),
                sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal),
                    sf.Literal("2", TGDLType.Decimal),
                    OperatorKind.LessThan
                ),
                OperatorKind.Equal
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
                        OperatorKind.Addition
                    ),
                    sf.Literal("1", TGDLType.Decimal),
                    OperatorKind.GreaterThan
                ),
                sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal),
                    sf.Literal("2", TGDLType.Decimal),
                    OperatorKind.LessThan
                ),
                OperatorKind.Equal
            )
        },
    };
}
