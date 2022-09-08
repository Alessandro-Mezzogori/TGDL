using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData;

internal class LiteralExpressionSyntaxTestingData : ParserDataList<LiteralExpressionSyntax>
{
    public override List<DataUnit<string, LiteralExpressionSyntax>> DataList => new()
    {
        new()
        {
            Input = "1",
            Output = new("1", TGDLType.Decimal)
        },
        new()
        {
            Input = " 1 ",
            Output = new("1", TGDLType.Decimal)
        },
        new()
        {
            Input = "a1",
            Output = new("1", TGDLType.Decimal),
            Test = TestType.Fail
        },
        new()
        {
            Input = "textbeforenumber1.0",
            Output = new("1.0", TGDLType.Decimal),
            Test = TestType.Fail
        },
        new()
        {
            Input = "   1.0",
            Output = new("1.0", TGDLType.Decimal)
        },
        new()
        {
            Input = "1.1020304050",
            Output = new("1.1020304050", TGDLType.Decimal)
        },
        new() // TODO more test for strings
        {
            Input = "\"string\"",
            Output = new("string", TGDLType.String)
        },
        new()
        {
            Input = "true",
            Output = new("true", TGDLType.Bool)
        },
        new()
        {
            Input = "false",
            Output = new("false", TGDLType.Bool)
        },
        new()
        {
            Input = "truefalse",
            Output = new("", TGDLType.Decimal),
            Test = TestType.Fail
        },
        new()
        {
            Input = "truetrue",
            Output = new("", TGDLType.Decimal),
            Test = TestType.Fail
        },
    };
}

internal class LiteralExpressionSyntaxComparer : IEqualityComparer<LiteralExpressionSyntax>
{
    public bool Equals(LiteralExpressionSyntax? x, LiteralExpressionSyntax? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        return x.Value == y.Value && x.Type.Type  == y.Type.Type;
    }

    public int GetHashCode([DisallowNull] LiteralExpressionSyntax obj)
    {
        return obj.GetHashCode();
    }
}
