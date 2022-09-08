using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class RequireLambdaExpressionDeclarationData : ParserDataList<RequireLambdaSyntaxDeclaration>
    {
        public override List<DataUnit<string, RequireLambdaSyntaxDeclaration>> DataList => new()
        {
        };
    }

    internal class RequireLambdaExpressionDeclarationComparer : IEqualityComparer<RequireLambdaSyntaxDeclaration>
    {
        private readonly IEqualityComparer<IEnumerable<ParameterSyntaxDeclaration>> _comparer = new ParametersSyntaxDeclarationComparer();
        // TODO Body comparer

        public bool Equals(RequireLambdaSyntaxDeclaration? x, RequireLambdaSyntaxDeclaration? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return false;
        }

        public int GetHashCode([DisallowNull] RequireLambdaSyntaxDeclaration obj)
        {
            return obj.GetHashCode();
        }
    }
}
