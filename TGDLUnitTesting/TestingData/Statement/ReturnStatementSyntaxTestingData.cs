using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData
{
    internal class ReturnStatementSyntaxTestingData : ParserDataList<ReturnStatementSyntax>
    {
        public override List<DataUnit<string, ReturnStatementSyntax>> DataList => new()
        {
            new()
            {
                Input = "return this.test",
                Output = new(
                    sf.BinaryOperation(
                        sf.IdentifierName("this"),
                        sf.IdentifierName("test"),
                        OperationKind.Dot
                    )
                )
            },
            new()
            {
                Input = "return test",
                Output = new(
                    sf.IdentifierName("test")
                )
            },
            new()
            {
                Input = "return 1 + 1",
                Output = new(
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("1", TGDLType.Decimal),
                        OperationKind.Addition
                    )
                )
            },
            new()
            {
                Input = "return 1",
                Output = new(
                    sf.Literal("1", TGDLType.Decimal)
                )
            },
            new()
            {
                Input = "1",
                Test = TestType.Fail
            },

        };
    }

    internal class ReturnStatementSyntaxComparer : IEqualityComparer<ReturnStatementSyntax>
    {
        private ExpressionSyntaxComparer _comparer = new();
        public bool Equals(ReturnStatementSyntax? x, ReturnStatementSyntax? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return _comparer.Equals(x.Expression, y.Expression);
        }

        public int GetHashCode([DisallowNull] ReturnStatementSyntax obj)
        {
            return obj.GetHashCode();
        }
    }
}
