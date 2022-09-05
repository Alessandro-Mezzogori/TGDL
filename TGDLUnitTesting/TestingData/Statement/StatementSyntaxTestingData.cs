using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData;

internal class StatementSyntaxTestingData : ParserDataList<StatementSyntax>
{
    public override List<DataUnit<string, StatementSyntax>> DataList => throw new NotImplementedException();
}

internal class StatementSyntaxComparer : IEqualityComparer<StatementSyntax>
{

    public bool Equals(StatementSyntax? x, StatementSyntax? y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;
        
        if(x is ReturnStatementSyntax xRet && y is ReturnStatementSyntax yRet)
        {
            return new ReturnStatementSyntaxComparer().Equals(xRet, yRet);
        }

        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] StatementSyntax obj)
    {
        return obj.GetHashCode();
    }
}
