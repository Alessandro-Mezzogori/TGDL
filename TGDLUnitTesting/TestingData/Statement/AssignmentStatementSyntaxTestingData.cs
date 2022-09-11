using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax.Statements;

namespace TGDLUnitTesting.TestingData;

using sf = TGDLLib.Syntax.SyntaxFactory;

internal class AssignmentStatementSyntaxTestingData : ParserDataList<AssignmentStatementSyntaxTestingData>
{
    public override List<DataUnit<string, AssignmentStatementSyntaxTestingData>> DataList => new()
    {

    };
}

internal class AssignmentStatementSyntaxComparer : IEqualityComparer<AssignmentStatementSyntax>
{
    private readonly ExpressionSyntaxComparer _expressionComparer = new();
    public bool Equals(AssignmentStatementSyntax? x, AssignmentStatementSyntax? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        return x.Identifier.Equals(y.Identifier)
            && _expressionComparer.Equals(x.Expression, y.Expression);
    }

    public int GetHashCode([DisallowNull] AssignmentStatementSyntax obj)
    {
        return obj.GetHashCode();
    }
}
