using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData
{
    internal class BinaryOperationExpressionSyntaxTestingData : ParserDataList<ExpressionSyntax>
    {
        public override List<DataUnit<string, ExpressionSyntax>> DataList => new()
        {
            new()
            {
                Input = "1 + 2",
                Output = sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal), 
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.Addition
                ),
            },
            new()
            {
                Input = "(1)",
                Output = sf.Literal("1", TGDLType.Decimal), 
            },
            new()
            {
                Input = "(1) + (2)",
                Output = sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal), 
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.Addition
                ),
            },
            new()
            {
                Input = "(this.access) + (2)",
                Output =  sf.BinaryOperation(
                    sf.BinaryOperation(
                        sf.IdentifierName("this"), 
                        sf.IdentifierName("access"),
                        OperationKind.AttributeAccess
                    ),
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.Addition
                ),
            },
            new()
            {
                Input = "(3 + 2)",
                Output = sf.BinaryOperation(
                    sf.Literal("3", TGDLType.Decimal), 
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.Addition 
                ),
            },
            new()
            {
                Input = "3 + (1 + 2)",
                Output = sf.BinaryOperation(
                    sf.Literal("3", TGDLType.Decimal), 
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("2", TGDLType.Decimal),
                        OperationKind.Addition
                    ),
                    OperationKind.Addition
                ),
            },
            new()
            {
                Input = "3 - (1 + 2)",
                Output = sf.BinaryOperation(
                    sf.Literal("3", TGDLType.Decimal), 
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("2", TGDLType.Decimal),
                        OperationKind.Addition
                    ),
                    OperationKind.Subtraction
                ),
            },
            new()
            {
                Input = "-3 - (1 - 2)",
                Output = sf.BinaryOperation(
                    sf.Literal("-3", TGDLType.Decimal), 
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("2", TGDLType.Decimal),
                        OperationKind.Subtraction
                    ),
                    OperationKind.Subtraction
                ),
            },
            new()
            {
                Input = "3 - (1 - 2 + 2)",
                Output = sf.BinaryOperation(
                    sf.Literal("3", TGDLType.Decimal), 
                    sf.BinaryOperation(
                        sf.BinaryOperation(
                            sf.Literal("1", TGDLType.Decimal),
                            sf.Literal("2", TGDLType.Decimal),
                            OperationKind.Subtraction
                        ),
                        sf.Literal("2", TGDLType.Decimal),
                        OperationKind.Addition
                    ),
                    OperationKind.Subtraction
                ),
            },
            new() // TODO convert with SyntaxFactory
            {
                Input = "3 + (1 - (2 + 1))",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal), 
                    OperationKind.Addition, 
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperationKind.Subtraction,
                        new BinaryOperationExpressionSyntax( 
                            new LiteralExpressionSyntax("2", TGDLType.Decimal),
                            OperationKind.Addition,
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
                    OperationKind.Division, 
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperationKind.Moltiplication,
                        new BinaryOperationExpressionSyntax( 
                            new LiteralExpressionSyntax("2", TGDLType.Decimal),
                            OperationKind.Modulo,
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
                        OperationKind.Division, 
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    ),
                    OperationKind.Division,
                    new LiteralExpressionSyntax("1", TGDLType.Decimal)
                ),
            },
            new()
            {
                Input = "3 ^ 2 ^ 1",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("3", TGDLType.Decimal),
                    OperationKind.Power,
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("2", TGDLType.Decimal),
                        OperationKind.Power,
                        new LiteralExpressionSyntax("1", TGDLType.Decimal)
                    )
                )
            },
            new()
            {
                Input = "1 - 3 ^ 2 ^ 1 mod 2",
                Output = new BinaryOperationExpressionSyntax(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal),
                    OperationKind.Subtraction,
                    new BinaryOperationExpressionSyntax(
                        new BinaryOperationExpressionSyntax(
                            new LiteralExpressionSyntax("3", TGDLType.Decimal),
                            OperationKind.Power,
                            new BinaryOperationExpressionSyntax(
                                new LiteralExpressionSyntax("2", TGDLType.Decimal),
                                OperationKind.Power,
                                new LiteralExpressionSyntax("1", TGDLType.Decimal)
                            )
                        ),
                        OperationKind.Modulo,
                        new LiteralExpressionSyntax("2", TGDLType.Decimal)
                    )
                )
            },
            new() // TODO remove this. support ?
            {
                Input = "this.access ^ 1 ^ 2",
                Output = sf.BinaryOperation(
                    sf.BinaryOperation(
                        sf.IdentifierName("this"), 
                        sf.IdentifierName("access"),
                        OperationKind.AttributeAccess
                    ),
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("2", TGDLType.Decimal),
                        OperationKind.Power
                    ),
                    OperationKind.Power
                )
            },
            new()
            {
                Input = "test.a.b",
                Output = sf.BinaryOperation(
                    sf.BinaryOperation(
                        sf.IdentifierName("test"),
                        sf.IdentifierName("a"),
                        OperationKind.AttributeAccess
                    ),
                    sf.IdentifierName("b"),
                    OperationKind.AttributeAccess
                )
            },
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
