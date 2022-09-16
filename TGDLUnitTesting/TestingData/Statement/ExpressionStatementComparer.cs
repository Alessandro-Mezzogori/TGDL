using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax.Statements;

namespace TGDLUnitTesting.TestingData;

internal class ExpressionStatementComparer : IEqualityComparer<ExpressionStatement>
{
    private readonly ExpressionSyntaxComparer _comparer = new();
    public new bool Equals(ExpressionStatement? x, ExpressionStatement? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        return _comparer.Equals(x.Expression, y.Expression);
    }

    public int GetHashCode([DisallowNull] ExpressionStatement obj)
    {
        return obj.GetHashCode(); 
    }
}