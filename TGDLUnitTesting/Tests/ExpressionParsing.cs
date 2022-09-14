using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using TGDLLib;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

public class ExpressionParsing
{
    [Theory, ClassData(typeof(LiteralExpressionSyntaxTestingData))]
    public void LiteralExpressionSyntaxTests(DataUnit<string, LiteralExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionTester().Visit(x), new ExpressionSyntaxComparer());
    }

    [Theory, ClassData(typeof(ExpressionSyntaxTestingData))]
    public void ExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionTester().Visit(x), new ExpressionSyntaxComparer());
    }

    [Theory, ClassData(typeof(BinaryOperationExpressionSyntaxTestingData))]
    public void MathOperationExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionTester().Visit(x), new ExpressionSyntaxComparer());
    }

    [Theory, ClassData(typeof(ComparisonBinaryExpressionSyntaxTestingData))]
    public void ComparisonOperationExpressionSyntaxTest(DataUnit<string, BinaryOperationExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionTester().Visit(x), new ExpressionSyntaxComparer());
    }

    [Theory, ClassData(typeof(EqualityBinaryOperationExpressionSyntaxTestingData))]
    public void EqualityExpressionSyntaxTest(DataUnit<string, ExpressionSyntax> unit)
    {
        ParseTests.Test(unit, x => x.expression(), x => new ExpressionTester().Visit(x), new ExpressionSyntaxComparer());
    }
}

public static class ParseTests
{
    public static void Test<T>(IDataUnit<string, T> unit, Func<TGDLParser, IParseTree> selector, Func<IParseTree, T> visitor, IEqualityComparer<T>? comparer = null)
    {
        using TextReader reader = new StringReader(unit.Input);
        var antlrInput = new AntlrInputStream(reader);
        var lexer = new TGDLLexer(antlrInput);
        var tokens = new CommonTokenStream(lexer);
        var parser = new TGDLParser(tokens);

        IParseTree tree = selector(parser);
        var result = visitor(tree);

        comparer ??= EqualityComparer<T>.Default;

        if(unit.Output == null)
        {
            if (unit.Test != TestType.Fail)
                throw new ArgumentException("unit.Output can only be null if Test = TestType.Fail");

            Assert.True(parser.NumberOfSyntaxErrors > 0);
            // TODO ErrorListener to show syntax errors;
            // check errors
        }

        switch (unit.Test)
        {
            case TestType.Equal:
                Assert.Equal(unit.Output, result, comparer);
                break;
            case TestType.NotEqual:
                Assert.NotEqual(unit.Output, result, comparer);
                break;
        }
    }
}
