using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class ParameterSyntaxDeclarationData : ParserDataList<ParameterSyntaxDeclaration>
    {
        public override List<DataUnit<string, ParameterSyntaxDeclaration>> DataList => new()
        {
            new(){
                Input = "bool testBool",
                Output = new (
                    new ("bool"),
                    new ("testBool"))
            },
            new(){
                Input = "int testInt",
                Output = new (
                    new ("bool"),
                    new ("testInt")),
                Test = TestType.NotEqual
            },
            new(){
                Input = "string testString",
                Output = new (
                    new ("string"),
                    new ("testString"))
            },
        };
    }

    internal class ParameterSyntaxDeclarationComparer : IEqualityComparer<ParameterSyntaxDeclaration>
    {
        public bool Equals(ParameterSyntaxDeclaration? x, ParameterSyntaxDeclaration? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.Identifier.Identifier == y.Identifier.Identifier
                   && x.Type.Type == y.Type.Type;
        }

        public int GetHashCode([DisallowNull] ParameterSyntaxDeclaration obj)
        {
            return obj.GetHashCode();
        }
    }
}
