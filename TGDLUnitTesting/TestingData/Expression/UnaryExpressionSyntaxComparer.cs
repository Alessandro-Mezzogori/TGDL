using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData.Expression;

internal class UnaryExpressionSyntaxComparer : IEqualityComparer<UnaryExpressionSyntax>
{
    private readonly ExpressionSyntaxComparer _expressionComparer = new();
    public bool Equals(UnaryExpressionSyntax? x, UnaryExpressionSyntax? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        return x.Operation == y.Operation && _expressionComparer.Equals(x.Operand, y.Operand);
    }

    public int GetHashCode([DisallowNull] UnaryExpressionSyntax obj)
    {
        return obj.GetHashCode();
    }
}

