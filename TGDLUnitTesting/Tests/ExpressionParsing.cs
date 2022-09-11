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

        [Theory, ClassData(typeof(ExpressionSyntaxTestingData))]
        public void ExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(unit, Grammar.Expressions.Expression, new ExpressionSyntaxComparer());
           
        }

        [Theory, ClassData(typeof(BinaryOperationExpressionSyntaxTestingData))]
        public void MathOperationExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(
                unit,
                Grammar.Expressions.Expression,
                new ExpressionSyntaxComparer()
            );
        }

        [Theory, ClassData(typeof(ComparisonBinaryExpressionSyntaxTestingData))]
        public void ComparisonOperationExpressionSyntaxTest(DataUnit<string, BinaryOperationExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(
                unit,
                Grammar.Expressions.Expression,
                new ExpressionSyntaxComparer()
            );
        }

        [Theory, ClassData(typeof(EqualityBinaryOperationExpressionSyntaxTestingData))]
        public void EqualityExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
        {
            TestingHelpers.TestParsingDataUnit(unit, Grammar.Expressions.Expression, new ExpressionSyntaxComparer());
        }
    }
}
