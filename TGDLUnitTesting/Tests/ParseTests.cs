using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using TGDLLib;
using TGDLLib.Visitors;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

public static class ParseTests
{
    public static void Test<T>(IDataUnit<string, T> unit, Func<TGDLParser, IParseTree> selector, Func<IParseTree, T> visitor, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;

        using TextReader reader = new StringReader(unit.Input);
        var antlrInput = new AntlrInputStream(reader);
        var lexer = new TGDLLexer(antlrInput);
        var tokens = new CommonTokenStream(lexer);
        var parser = new TGDLParser(tokens);

        // TODO add TestErrorListener for specific syntax errors


        IParseTree tree = selector(parser);
        try
        {
            var result = visitor(tree);
            //parse exceptino

            if (unit.Output == null)
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
        catch (ParseException e)
        {
            if (unit.Test != TestType.Fail) 
                throw e;
        }
    }
}
