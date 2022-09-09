using Sprache;
using TGDLLib;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting
{
    public class ExpressionParsing
    {
        [Theory, ClassData(typeof(LiteralExpressionSyntaxTestingData))]
        public void LiteralExpressionSyntaxTests(DataUnit<string, LiteralExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(unit, Grammar.Expressions.LiteralExpression, new LiteralExpressionSyntaxComparer());
        }

        [Theory, ClassData(typeof(MemberAccessExpressionSyntaxTestingData))]
        public void MemberAccessExpressionSyntaxTest(DataUnit<string, AttributeAccessExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(unit, Grammar.Expressions.MemberAccessExpression, new MemberAccessExpressionSyntaxComparer());
        }

        [Theory, ClassData(typeof(ExpressionSyntaxTestingData))]
        public void ExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(unit, Grammar.Expressions.Expression, new ExpressionSyntaxComparer());
           
        }

        [Theory, ClassData(typeof(OperationExpressionSyntaxTestingData))]
        public void OperationExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(
                unit,
                Grammar.Expressions.OperationExpression,
                new OperationExpressionSyntaxComparer()
            );
        }

        [Theory, ClassData(typeof(ComparisonExpressionSyntaxTestingData))]
        public void ComparisonExpressionSyntaxTest(DataUnit<string, ComparisonExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(
                unit,
                Grammar.Expressions.ComparisonExpression,
                new ComparisonExpressionSyntaxComparer()
            );
        }
    }
}
