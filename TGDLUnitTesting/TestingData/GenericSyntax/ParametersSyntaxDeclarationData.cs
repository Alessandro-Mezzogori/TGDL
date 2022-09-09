using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    // TODO Remove default predefined types in parameters list
    // only state or special predefined can be 
    internal class ParametersSyntaxDeclarationData : ParserDataList<IEnumerable<ParameterSyntaxDeclaration>>
    {
        public override List<DataUnit<string, IEnumerable<ParameterSyntaxDeclaration>>> DataList => new()
        {
            new(){
                Input = "bool Bool, decimal Int, player Player",
                Output = new List<ParameterSyntaxDeclaration>
                {
                    new(SyntaxFactory.PredefinedType(TGDLType.Bool), new("Bool")),
                    new(SyntaxFactory.PredefinedType(TGDLType.Decimal), new("Int")),
                    new(SyntaxFactory.SuppliedPredefinedType(TGDLType.Player), new("Player")),
                }
            },
            new(){
                Input = "bool Bool",
                Output = new List<ParameterSyntaxDeclaration>
                {
                    new(SyntaxFactory.PredefinedType(TGDLType.Bool), new("Bool")),
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
