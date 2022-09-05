using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

internal class BodySyntaxDeclarationTestingData : ParserDataList<BodySyntaxDeclaration>
{
    public override List<DataUnit<string, BodySyntaxDeclaration>> DataList => new()
    {
        new()
        {
            Input
        }
    };
}

internal class BodySyntaxDeclarationComparer : IEqualityComparer<BodySyntaxDeclaration>
{
    private readonly StatementSyntaxComparer _comparer = new();

    public bool Equals(BodySyntaxDeclaration? x, BodySyntaxDeclaration? y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;

        if (x.Statements.Count() != y.Statements.Count()) return false;


        return x.Statements.All(x => y.Statements.Contains(x, _comparer));
    }

    public int GetHashCode([DisallowNull] BodySyntaxDeclaration obj)
    {
        return obj.GetHashCode();
    }
}
