using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class OperationExpressionSyntaxTestingData : ParserDataList<ExpressionSyntax>
    {
        public override List<DataUnit<string, ExpressionSyntax>> DataList => new()
        {
            new()
            {
                Input = "1 + 2",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal), 
                    Operation.Addition, 
                    new LiteralExpressionSyntax("2", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "(3 + 2)",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    Operation.Addition, 
                    new LiteralExpressionSyntax("2", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "3 + (1 + 2)",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    Operation.Addition, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        Operation.Addition,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                ),
            },
            new()
            {
                Input = "3 - (1 + 2)",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    Operation.Subtraction, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        Operation.Addition,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                ),
            },
            new()
            {
                Input = "-3 - (1 - 2)",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("-3", TGDLType.Decimal), 
                    Operation.Subtraction, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        Operation.Subtraction,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                ),
            },
            new()
            {
                Input = "3 - (1 - 2 + 2)",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    Operation.Subtraction, 
                    new OperationExpressionSyntax(
                        new OperationExpressionSyntax(
                            new LiteralExpressionSyntax("1", TGDLType.Decimal),
                            Operation.Subtraction,
                            new LiteralExpressionSyntax("2", TGDLType.Decimal)
                        ),
                        Operation.Addition,
                        new LiteralExpressionSyntax ("2", TGDLType.Decimal)
                    )
                ),
            },
            new()
            {
                Input = "3 + (1 - (2 + 1))",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    Operation.Addition, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        Operation.Subtraction,
                        new OperationExpressionSyntax( 
                            new LiteralExpressionSyntax("2", TGDLType.Decimal),
                            Operation.Addition,
                            new LiteralExpressionSyntax("1", TGDLType.Decimal)
                        )
                    )
                ),
            },
            new()
            {
                Input = "3 / (1 * (2 mod 1))",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    Operation.Division, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        Operation.Moltiplication,
                        new OperationExpressionSyntax( 
                            new LiteralExpressionSyntax("2", TGDLType.Decimal),
                            Operation.Modulo,
                            new LiteralExpressionSyntax("1", TGDLType.Decimal)
                        )
                    )
                ),
            },
            new()
            {
                Input = "3 / 2 / 1 )",
                Output = new OperationExpressionSyntax(
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                        Operation.Division, 
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    ),
                    Operation.Division,
                    new LiteralExpressionSyntax("1", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "3 ^ 2 ^ 1",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal),
                    Operation.Power,
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("2", TGDLType.Decimal),
                        Operation.Power,
                        new LiteralExpressionSyntax("1", TGDLType.Decimal)
                    )
                )
            },
            new()
            {
                Input = "1 - 3 ^ 2 ^ 1 mod 2",
                Output = new OperationExpressionSyntax(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal),
                    Operation.Subtraction,
                    new OperationExpressionSyntax(
                        new OperationExpressionSyntax(
                            new LiteralExpressionSyntax("3", TGDLType.Decimal),
                            Operation.Power,
                            new OperationExpressionSyntax(
                                new LiteralExpressionSyntax("2", TGDLType.Decimal),
                                Operation.Power,
                                new LiteralExpressionSyntax("1", TGDLType.Decimal)
                            )
                        ),
                        Operation.Modulo,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                )
            },
            new()
            {
                Input = "this.access ^ 1 ^ 2",
                Output = new OperationExpressionSyntax(
                    new AttributeAccessExpressionSyntax(new("this"), new("access")),
                    Operation.Power,
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        Operation.Power,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                  )
                )
            }

        };
    }

    internal class OperationExpressionSyntaxComparer : IEqualityComparer<ExpressionSyntax>
    {
        private readonly ExpressionSyntaxComparer _comparer = new();
        public bool Equals(ExpressionSyntax? x, ExpressionSyntax? y)
        {
            if(x == null && y == null) return true;
            if(x == null || y == null) return false;
            if (x is not OperationExpressionSyntax xOp || y is not OperationExpressionSyntax yOp) return false;

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
