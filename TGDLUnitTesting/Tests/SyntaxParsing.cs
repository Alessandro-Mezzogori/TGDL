using TGDLLib;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;


public class SyntaxParsing
{ 
    [Theory]
    [ClassData(typeof(ParameterSyntaxDeclarationData))]
    public void ParameterSyntaxDeclarationTest(DataUnit<string, ParameterSyntaxDeclaration> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Parsers.ParameterSyntax, new ParameterSyntaxDeclarationComparer());
    }

    [Theory]
    [ClassData(typeof(ParametersSyntaxDeclarationData))]
    public void ParametersSyntaxDeclarationTest(DataUnit<string, IEnumerable<ParameterSyntaxDeclaration>> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Parsers.ParametersSyntax, new ParametersSyntaxDeclarationComparer());
    }

    public void RequireLambdaExpressionDeclaration(DataUnit<string, RequireLambdaExpressionDeclaration> unit)
    {
    }
}
