using Sprache;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

internal static class TestingHelpers 
{
    public static void TestParsingDataUnit<TOutput>(DataUnit<string, TOutput> unit, Parser<TOutput> Parser, Func<TOutput, TOutput, bool> comparer)
        where TOutput: notnull
    {
        Func<string, TOutput> conversionFunction = x => Parser.Parse(x);

        switch (unit.Test)
        {
            case TestType.Equal:
                Assert.True(comparer(unit.Output, conversionFunction(unit.Input)));
                break;
            case TestType.NotEqual:
                Assert.False(comparer(unit.Output, conversionFunction(unit.Input)));
                break;
            case TestType.Fail:
                Assert.Throws<ParseException>(() => conversionFunction(unit.Input));
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public static void TestParsingDataUnit<TOutput>(DataUnit<string, TOutput> unit, Parser<TOutput> Parser, IEqualityComparer<TOutput>? comparer)
        where TOutput: notnull
    {
        Func<string, TOutput> conversionFunction = x => Parser.Parse(x);
        comparer ??= EqualityComparer<TOutput>.Default;

        switch (unit.Test)
        {
            case TestType.Equal:
                Assert.Equal(unit.Output, conversionFunction(unit.Input), comparer);
                break;
            case TestType.NotEqual:
                Assert.NotEqual(unit.Output, conversionFunction(unit.Input), comparer);
                break;
            case TestType.Fail:
                Assert.Throws<ParseException>(() => conversionFunction(unit.Input));
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
