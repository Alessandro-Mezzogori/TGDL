using TGDLLib;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

public class StatementParsing
{
    [Theory, ClassData(typeof(ReturnStatementSyntaxTestingData))]
    public void ReturnStatementSyntax(DataUnit<string, ReturnStatementSyntax> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Grammar.Statements.ReturnStatement, new ReturnStatementSyntaxComparer());
    }
}
