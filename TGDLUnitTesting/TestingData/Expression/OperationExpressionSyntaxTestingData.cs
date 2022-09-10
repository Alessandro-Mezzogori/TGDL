using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class BinaryOperationExpressionSyntaxTestingData : ParserDataList<ExpressionSyntax>
    {
        public override List<DataUnit<string, ExpressionSyntax>> DataList => new()
        {
            new()
            {
                Input = "1 + 2",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal), 
                    OperatorKind.Addition, 
                    new LiteralExpressionSyntax("2", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "(1)",
                Output = new LiteralExpressionSyntax("1", TGDLType.Decimal), 
            },
            new()
            {
                Input = "(1) + (2)",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal), 
                    OperatorKind.Addition, 
                    new LiteralExpressionSyntax("2", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "(this.access) + (2)",
                Output = new BinaryOperationExpressionSyntax(
                    new AttributeAccessExpressionSyntax(new("this"), new("access")), 
                    OperatorKind.Addition, 
                    new LiteralExpressionSyntax("2", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "(3 + 2)",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    OperatorKind.Addition, 
                    new LiteralExpressionSyntax("2", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "3 + (1 + 2)",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    OperatorKind.Addition, 
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperatorKind.Addition,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                ),
            },
            new()
            {
                Input = "3 - (1 + 2)",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    OperatorKind.Subtraction, 
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperatorKind.Addition,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                ),
            },
            new()
            {
                Input = "-3 - (1 - 2)",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("-3", TGDLType.Decimal), 
                    OperatorKind.Subtraction, 
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperatorKind.Subtraction,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                ),
            },
            new()
            {
                Input = "3 - (1 - 2 + 2)",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    OperatorKind.Subtraction, 
                    new BinaryOperationExpressionSyntax(
                        new BinaryOperationExpressionSyntax(
                            new LiteralExpressionSyntax("1", TGDLType.Decimal),
                            OperatorKind.Subtraction,
                            new LiteralExpressionSyntax("2", TGDLType.Decimal)
                        ),
                        OperatorKind.Addition,
                        new LiteralExpressionSyntax ("2", TGDLType.Decimal)
                    )
                ),
            },
            new()
            {
                Input = "3 + (1 - (2 + 1))",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    OperatorKind.Addition, 
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperatorKind.Subtraction,
                        new BinaryOperationExpressionSyntax( 
                            new LiteralExpressionSyntax("2", TGDLType.Decimal),
                            OperatorKind.Addition,
                            new LiteralExpressionSyntax("1", TGDLType.Decimal)
                        )
                    )
                ),
            },
            new()
            {
                Input = "3 / (1 * (2 mod 1))",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    OperatorKind.Division, 
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperatorKind.Moltiplication,
                        new BinaryOperationExpressionSyntax( 
                            new LiteralExpressionSyntax("2", TGDLType.Decimal),
                            OperatorKind.Modulo,
                            new LiteralExpressionSyntax("1", TGDLType.Decimal)
                        )
                    )
                ),
            },
            new()
            {
                Input = "3 / 2 / 1 )",
                Output = new BinaryOperationExpressionSyntax(
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                        OperatorKind.Division, 
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    ),
                    OperatorKind.Division,
                    new LiteralExpressionSyntax("1", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "3 ^ 2 ^ 1",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal),
                    OperatorKind.Power,
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("2", TGDLType.Decimal),
                        OperatorKind.Power,
                        new LiteralExpressionSyntax("1", TGDLType.Decimal)
                    )
                )
            },
            new()
            {
                Input = "1 - 3 ^ 2 ^ 1 mod 2",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal),
                    OperatorKind.Subtraction,
                    new BinaryOperationExpressionSyntax(
                        new BinaryOperationExpressionSyntax(
                            new LiteralExpressionSyntax("3", TGDLType.Decimal),
                            OperatorKind.Power,
                            new BinaryOperationExpressionSyntax(
                                new LiteralExpressionSyntax("2", TGDLType.Decimal),
                                OperatorKind.Power,
                                new LiteralExpressionSyntax("1", TGDLType.Decimal)
                            )
                        ),
                        OperatorKind.Modulo,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                )
            },
            new()
            {
                Input = "this.access ^ 1 ^ 2",
                Output = new BinaryOperationExpressionSyntax(
                    new AttributeAccessExpressionSyntax(new("this"), new("access")),
                    OperatorKind.Power,
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperatorKind.Power,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                  )
                )
            }

        };
    }
    internal class BinaryOperationExpressionSyntaxComparer : IEqualityComparer<ExpressionSyntax>
    {
        private readonly ExpressionSyntaxComparer _comparer = new();
        public bool Equals(ExpressionSyntax? x, ExpressionSyntax? y)
        {
            if(x == null && y == null) return true;
            if(x == null || y == null) return false;
            if (x is not BinaryOperationExpressionSyntax xOp || y is not BinaryOperationExpressionSyntax yOp) return false;

            return _comparer.Equals(xOp.LeftOperand, yOp.LeftOperand) &&
                    xOp.Operation == yOp.Operation &&
                    _comparer.Equals(xOp.RightOperand, yOp.RightOperand);
        }

        public int GetHashCode([DisallowNull] ExpressionSyntax obj)
        {
            return obj.GetHashCode();
        }
    }
}
