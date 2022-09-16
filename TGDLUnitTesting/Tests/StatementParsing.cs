using TGDLLib;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Statements;
using TGDLLib.Visitors;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

public class StatementParsing
{
    [Theory, ClassData(typeof(StatementSyntaxTestingData))] 
    public void StatementSyntaxTests(DataUnit<string, StatementSyntax> unit)
    {
        ParseTests.Test(unit, parser => parser.statement(), tree => new StatementVisitor().Visit(tree), new StatementSyntaxComparer());
    }

    [Theory, ClassData(typeof(ReturnStatementSyntaxTestingData))]
    public void ReturnStatementSyntax(DataUnit<string, ReturnStatementSyntax> unit)
    {
        ParseTests.Test(unit, parser => parser.statement(), tree => new StatementVisitor().Visit(tree), new StatementSyntaxComparer());
    }

    [Theory, ClassData(typeof(AssignmentStatementSyntaxTestingData))]
    public void AssignmentStatementSyntax(DataUnit<string, AssignmentStatementSyntax> unit)
    {
        ParseTests.Test(unit, parser => parser.statement(), tree => new StatementVisitor().Visit(tree), new StatementSyntaxComparer());
    }


}
