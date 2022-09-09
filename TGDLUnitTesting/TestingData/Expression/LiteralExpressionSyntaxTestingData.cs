using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData.GenericSyntax;

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
            Input = "1.0",
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
            Input = "false2",
            Output = new("", TGDLType.Decimal),
            Test = TestType.Fail
        },
        new()
        {
            Input = "atruetrue",
            Output = new("", TGDLType.Decimal),
            Test = TestType.Fail
        },
        new() // logic error but parsing wise it is ok and it returns 1
        {
            Input = "1true",
            Output = new("1", TGDLType.Decimal),
        },
    };
}

internal class LiteralExpressionSyntaxComparer : IEqualityComparer<LiteralExpressionSyntax>
{
    private TypeSyntaxComparer _typeComparer = new();
    public bool Equals(LiteralExpressionSyntax? x, LiteralExpressionSyntax? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        return x.Value == y.Value && x.Type.Equals(y.Type); 
    }

    public int GetHashCode([DisallowNull] LiteralExpressionSyntax obj)
    {
        return obj.GetHashCode();
    }
}
