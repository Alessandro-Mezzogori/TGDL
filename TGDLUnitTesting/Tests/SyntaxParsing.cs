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
        TestingHelpers.TestParsingDataUnit(unit, Grammar.ParameterSyntax, new ParameterSyntaxDeclarationComparer());
    }

    [Theory]
    [ClassData(typeof(ParametersSyntaxDeclarationData))]
    public void ParametersSyntaxDeclarationTest(DataUnit<string, IEnumerable<ParameterSyntaxDeclaration>> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Grammar.ParametersSyntax, new ParametersSyntaxDeclarationComparer());
    }

    [Theory, ClassData(typeof(BodySyntaxDeclarationTestingData))]
    public void BodySyntaxDeclarationTest(DataUnit<string, BodySyntaxDeclaration> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Grammar.BodySyntax, new BodySyntaxDeclarationComparer());
    }

    public void RequireLambdaExpressionDeclaration(DataUnit<string, RequireLambdaExpressionDeclaration> unit)
    {
    }
}
