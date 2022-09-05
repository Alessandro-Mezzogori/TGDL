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
        comparer ??= EqualityComparer<TOutput>.Default;

        IResult<TOutput> result = Parser.TryParse(unit.Input);
        switch (unit.Test)
        {
            case TestType.Equal:
                Assert.True(result.WasSuccessful);
                Assert.Equal(unit.Output, result.Value, comparer);
                break;
            case TestType.NotEqual:
                Assert.True(result.WasSuccessful);
                Assert.NotEqual(unit.Output, result.Value, comparer);
                break;
            case TestType.Fail:
                Assert.False(result.WasSuccessful);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
