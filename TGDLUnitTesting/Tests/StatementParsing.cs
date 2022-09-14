using TGDLLib;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Statements;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

public class StatementParsing
{
    [Theory, ClassData(typeof(StatementSyntaxTestingData))] 
    public void StatementSyntaxTests(DataUnit<string, StatementSyntax> unit)
    {
    }

    [Theory, ClassData(typeof(ReturnStatementSyntaxTestingData))]
    public void ReturnStatementSyntax(DataUnit<string, ReturnStatementSyntax> unit)
    {
    }

    [Theory, ClassData(typeof(AssignmentStatementSyntaxTestingData))]
    public void AssignmentStatementSyntax(DataUnit<string, AssignmentStatementSyntax> unit)
    {
    }
}
