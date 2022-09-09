using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData.GenericSyntax;

namespace TGDLUnitTesting.TestingData
{
    internal class ParameterSyntaxDeclarationData : ParserDataList<ParameterSyntaxDeclaration>
    {
        public override List<DataUnit<string, ParameterSyntaxDeclaration>> DataList => new()
        {
            new(){
                Input = "bool testBool",
                Output = new (
                    new PredefinedTypeSyntax(TGDLType.Bool),
                    new ("testBool"))
            },
            new(){
                Input = "decimal testInt",
                Output = new (
                    new PredefinedTypeSyntax(TGDLType.Bool),
                    new ("testInt")),
                Test = TestType.NotEqual
            },
            new(){
                Input = "string testString",
                Output = new (
                    new PredefinedTypeSyntax(TGDLType.String),
                    new ("testString"))
            },
        };
    }

    internal class ParameterSyntaxDeclarationComparer : IEqualityComparer<ParameterSyntaxDeclaration>
    {
        private readonly TypeSyntaxComparer _typeComparer = new();

        public bool Equals(ParameterSyntaxDeclaration? x, ParameterSyntaxDeclaration? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.Identifier.Identifier == y.Identifier.Identifier && _typeComparer.Equals(x.Type, y.Type);
        }

        public int GetHashCode([DisallowNull] ParameterSyntaxDeclaration obj)
        {
            return obj.GetHashCode();
        }
    }
}
