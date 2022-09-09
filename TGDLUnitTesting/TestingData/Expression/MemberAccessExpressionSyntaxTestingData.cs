using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class MemberAccessExpressionSyntaxTestingData: ParserDataList<AttributeAccessExpressionSyntax>
    {
        public override List<DataUnit<string, AttributeAccessExpressionSyntax>> DataList => new()
        {
            new()
            {
                Input = "player.test",
                Output = new(new("player"), new("test"))
            },
            new()
            {
                Input = "player.",
                Output = new(new("player"), new("test")),
                Test = TestType.Fail
            },
        };
    }

    internal class MemberAccessExpressionSyntaxComparer : IEqualityComparer<AttributeAccessExpressionSyntax>
    {
        public bool Equals(AttributeAccessExpressionSyntax? x, AttributeAccessExpressionSyntax? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.Target.Identifier == y.Target.Identifier 
                && x.Member.Identifier == y.Member.Identifier;
        }

        public int GetHashCode([DisallowNull] AttributeAccessExpressionSyntax obj)
        {
            return obj.GetHashCode();
        }
    }
}
