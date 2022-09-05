using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class ParametersSyntaxDeclarationData : ParserDataList<IEnumerable<ParameterSyntaxDeclaration>>
    {
        public override List<DataUnit<string, IEnumerable<ParameterSyntaxDeclaration>>> DataList => new()
        {
            new(){
                Input = "bool Bool, int Int, player Player",
                Output = new List<ParameterSyntaxDeclaration>
                {
                    new(new("bool"), new("Bool")),
                    new(new("int"), new("Int")),
                    new(new("player"), new("Player"))
                }
            },
            new(){
                Input = "bool Bool, player Int, player Player",
                Output = new List<ParameterSyntaxDeclaration>
                {
                    new(new("bool"), new("Bool")),
                    new(new("int"), new("Int")),
                    new(new("player"), new("Player"))
                },
                Test = TestType.NotEqual
            },
            new(){
                Input = "bool Bool",
                Output = new List<ParameterSyntaxDeclaration>
                {
                    new(new("bool"), new("Bool")),
                },
                Test = TestType.Equal      
            },
        };
    }

    internal class ParametersSyntaxDeclarationComparer : IEqualityComparer<IEnumerable<ParameterSyntaxDeclaration>>
    {
        private readonly IEqualityComparer<ParameterSyntaxDeclaration> _comparer = new ParameterSyntaxDeclarationComparer();

        public bool Equals(IEnumerable<ParameterSyntaxDeclaration>? x, IEnumerable<ParameterSyntaxDeclaration>? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return x.Count() == y.Count() && x.All(xParam => y.Any(yParam => _comparer.Equals(xParam, yParam)));
        }

        public int GetHashCode([DisallowNull] IEnumerable<ParameterSyntaxDeclaration> obj)
        {
            return obj.GetHashCode();
        }
    }
}
