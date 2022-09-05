using Sprache;
using TGDLLib;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting.Tests
{
    public class ExpressionParsing
    {
        [Theory, ClassData(typeof(MemberAccessExpressionSyntaxTestingData))]
        public void MemberAccessExpressionSyntaxTest(DataUnit<string, MemberAccessExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(unit, Parsers.Expressions.MemberAccessExpression, new MemberAccessExpressionSyntaxComparer());
        }

        [Theory, ClassData(typeof(ExpressionSyntaxTestingData))]
        public void ExpressionSyntaxDifferentTypesTest(DataUnit<string, ExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(unit, Parsers.Expressions.Expression, (expected, actual) =>
            {
                return expected.GetType() == actual.GetType();
            });
        }

        [Theory, ClassData(typeof(OperationExpressionSyntaxTestingData))]
        public void OperationExpressionSyntaxTest(DataUnit<string, OperationExpressionSyntax> unit)
        {
            var parsed = Parsers.Expressions.OperationExpression.Parse(unit.Input);

            TestingHelpers.TestParsingDataUnit(
                unit,
                Parsers.Expressions.OperationExpression,
                new OperationExpressionSyntaxComparer()
            );
        }
    }
}
