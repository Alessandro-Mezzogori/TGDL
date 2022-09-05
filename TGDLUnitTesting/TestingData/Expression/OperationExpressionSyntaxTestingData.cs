using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class OperationExpressionSyntaxTestingData : ParserDataList<OperationExpressionSyntax>
    {
        public override List<DataUnit<string, OperationExpressionSyntax>> DataList => new()
        {
            new()
            {
                Input = "1 + 2",
                Output = new(
                    new LiteralExpressionSyntax("1", LiteralType.Integer), 
                    Operation.Addition, 
                    new LiteralExpressionSyntax("2", LiteralType.Integer)
                ),
            },
            new()
            {
                Input = "(3 + 2)",
                Output = new(
                    new LiteralExpressionSyntax("3", LiteralType.Integer), 
                    Operation.Addition, 
                    new LiteralExpressionSyntax("2", LiteralType.Integer)
                ),
            },
            new()
            {
                Input = "3 + (1 + 2)",
                Output = new(
                    new LiteralExpressionSyntax("3", LiteralType.Integer), 
                    Operation.Addition, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", LiteralType.Integer),
                        Operation.Addition,
                        new LiteralExpressionSyntax("2", LiteralType.Integer)
                    )
                ),
            },
            new()
            {
                Input = "3 - (1 + 2)",
                Output = new(
                    new LiteralExpressionSyntax("3", LiteralType.Integer), 
                    Operation.Subtraction, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", LiteralType.Integer),
                        Operation.Addition,
                        new LiteralExpressionSyntax("2", LiteralType.Integer)
                    )
                ),
            },
            new()
            {
                Input = "-3 - (1 - 2)",
                Output = new(
                    new LiteralExpressionSyntax("-3", LiteralType.Integer), 
                    Operation.Subtraction, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", LiteralType.Integer),
                        Operation.Subtraction,
                        new LiteralExpressionSyntax("2", LiteralType.Integer)
                    )
                ),
            },
            new()
            {
                Input = "3 - (1 - 2 + 2)",
                Output = new(
                    new LiteralExpressionSyntax("3", LiteralType.Integer), 
                    Operation.Subtraction, 
                    new OperationExpressionSyntax(
                        new OperationExpressionSyntax(
                            new LiteralExpressionSyntax("1", LiteralType.Integer),
                            Operation.Subtraction,
                            new LiteralExpressionSyntax("2", LiteralType.Integer)
                        ),
                        Operation.Addition,
                        new LiteralExpressionSyntax ("2", LiteralType.Integer)
                    )
                ),
            },
            new()
            {
                Input = "3 + (1 - (2 + 1))",
                Output = new(
                    new LiteralExpressionSyntax("3", LiteralType.Integer), 
                    Operation.Addition, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", LiteralType.Integer),
                        Operation.Subtraction,
                        new OperationExpressionSyntax( 
                            new LiteralExpressionSyntax("2", LiteralType.Integer),
                            Operation.Addition,
                            new LiteralExpressionSyntax("1", LiteralType.Integer)
                        )
                    )
                ),
            },
            new()
            {
                Input = "3 / (1 * (2 mod 1))",
                Output = new(
                    new LiteralExpressionSyntax("3", LiteralType.Integer), 
                    Operation.Division, 
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", LiteralType.Integer),
                        Operation.Moltiplication,
                        new OperationExpressionSyntax( 
                            new LiteralExpressionSyntax("2", LiteralType.Integer),
                            Operation.Modulo,
                            new LiteralExpressionSyntax("1", LiteralType.Integer)
                        )
                    )
                ),
            },
            new()
            {
                Input = "3 / 2 / 1 )",
                Output = new(
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("3", LiteralType.Integer), 
                        Operation.Division, 
                        new LiteralExpressionSyntax("2", LiteralType.Integer)
                    ),
                    Operation.Division,
                    new LiteralExpressionSyntax("1", LiteralType.Integer)
                ),
            },
            new()
            {
                Input = "3 ^ 2 ^ 1",
                Output = new(
                    new LiteralExpressionSyntax("3", LiteralType.Integer),
                    Operation.Power,
                    new OperationExpressionSyntax(
                        new LiteralExpressionSyntax("2", LiteralType.Integer),
                        Operation.Power,
                        new LiteralExpressionSyntax("1", LiteralType.Integer)
                    )
                )
            }
        };
    }

    internal class OperationExpressionSyntaxComparer : IEqualityComparer<OperationExpressionSyntax>
    {
        private readonly ExpressionSyntaxComparer _comparer = new();
        public bool Equals(OperationExpressionSyntax? x, OperationExpressionSyntax? y)
        {
            if(x == null && y == null) return true;
            if(x == null || y == null) return false;

            return _comparer.Equals(x.LeftOperand, y.LeftOperand) &&
                    x.Operation == y.Operation &&
                    _comparer.Equals(x.RightOperand, y.RightOperand);
        }

        public int GetHashCode([DisallowNull] OperationExpressionSyntax obj)
        {
            return obj.GetHashCode();
        }
    }
}
