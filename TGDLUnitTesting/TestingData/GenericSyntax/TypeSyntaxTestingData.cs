using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData.GenericSyntax;

internal class TypeSyntaxTestingData : ParserDataList<TypeSyntax>
{
    public override List<DataUnit<string, TypeSyntax>> DataList => new()
    {

    };
}

internal class TypeSyntaxComparer : IEqualityComparer<TypeSyntax>
{
    public bool Equals(TypeSyntax? x, TypeSyntax? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        if (x is PredefinedTypeSyntax xPredefined && y is PredefinedTypeSyntax yPredefined)
        {
            return xPredefined.Equals(yPredefined);
        }
        else if (x is DeclaredTypeSyntax xDeclared && y is DeclaredTypeSyntax yDeclared)
        {
            return xDeclared.Equals(yDeclared);
        }

        return false;
    }

    public int GetHashCode([DisallowNull] TypeSyntax obj)
    {
        return obj.GetHashCode();
    }
}
