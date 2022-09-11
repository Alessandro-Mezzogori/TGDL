using Sprache;
using System.Data;
using TGDLUnitTesting.TestingData;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TGDLUnitTesting;

internal static class TestingHelpers 
{
    public static void TestParsingDataUnit<TOutput>(IDataUnit<string, TOutput> unit, Parser<TOutput> parser, Func<TOutput, TOutput, bool> comparer)
        where TOutput: notnull
    {
        IResult<TOutput> result = ParseUnit(unit, parser);


        Assert.True(unit.Test != TestType.Fail && unit.Output is not null);
        switch (unit.Test)
        {
            case TestType.Equal:
                Assert.True(result.WasSuccessful);
                Assert.True(comparer(unit.Output, result.Value));
                break;
            case TestType.NotEqual:
                Assert.True(result.WasSuccessful);
                Assert.False(comparer(unit.Output, result.Value));
                break;
            case TestType.Fail:
                Assert.False(result.WasSuccessful);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public static void TestParsingDataUnit<TOutput>(IDataUnit<string, TOutput> unit, Parser<TOutput> parser, IEqualityComparer<TOutput>? comparer)
        where TOutput: notnull
    {
        comparer ??= EqualityComparer<TOutput>.Default;

        try
        {
            IResult<TOutput> result = ParseUnit(unit, parser);
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
        catch(ParseException e)
        {
            if (unit.Test != TestType.Fail)
                throw e;
            return;
        }
   }

    private static IResult<TOutput> ParseUnit<TOutput>(IDataUnit<string, TOutput> unit, Parser<TOutput> parser) where TOutput : notnull
    {
        var result = parser.TryParse(unit.Input);
        if (!result.WasSuccessful && unit.Test != TestType.Fail)
        {
            Console.WriteLine(result.Message);
        }
        return result;
    }
}
