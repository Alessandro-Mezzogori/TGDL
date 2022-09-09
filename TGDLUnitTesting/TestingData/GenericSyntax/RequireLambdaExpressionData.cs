using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData.GenericSyntax;

namespace TGDLUnitTesting.TestingData
{
    internal class RequireSyntaxDeclarationTestingData : ParserDataList<RequireSyntaxDeclaration>
    {
        public override List<DataUnit<string, RequireSyntaxDeclaration>> DataList => new()
        {
        };
    }

    internal class RequireSyntaxDeclarationComparer : IEqualityComparer<RequireSyntaxDeclaration>
    {
        private readonly LambdaSyntaxDeclarationComparer _lambdaComparer = new();
        public bool Equals(RequireSyntaxDeclaration? x, RequireSyntaxDeclaration? y)
        {
            if(x == null && y == null) return true;
            if(x == null || y == null) return false;

            return x.Requirements.SequenceEqual(y.Requirements, _lambdaComparer);
        }

        public int GetHashCode([DisallowNull] RequireSyntaxDeclaration obj)
        {
            return obj.GetHashCode();
        }
    }
}
