using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class RequireLambdaExpressionDeclarationData : ParserDataList<RequireLambdaExpressionDeclaration>
    {
        public override List<DataUnit<string, RequireLambdaExpressionDeclaration>> DataList => new()
        {
        };
    }

    internal class RequireLambdaExpressionDeclarationComparer : IEqualityComparer<RequireLambdaExpressionDeclaration>
    {
        private readonly IEqualityComparer<IEnumerable<ParameterSyntaxDeclaration>> _comparer = new ParametersSyntaxDeclarationComparer();
        // TODO Body comparer

        public bool Equals(RequireLambdaExpressionDeclaration? x, RequireLambdaExpressionDeclaration? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return false;
        }

        public int GetHashCode([DisallowNull] RequireLambdaExpressionDeclaration obj)
        {
            return obj.GetHashCode();
        }
    }
}
