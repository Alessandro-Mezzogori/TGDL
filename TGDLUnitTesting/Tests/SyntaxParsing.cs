using TGDLLib;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData;
using TGDLUnitTesting.TestingData.GenericSyntax;

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

    [Theory, ClassData(typeof(AttributeSyntaxDeclarationTestingData))]
    public void AttributeSyntaxDeclarationTest(DataUnit<string, AttributeSyntaxDeclaration> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Grammar.Attribute, new AttributeSyntaxDeclarationComparer());
    }

    [Theory, ClassData(typeof(StateSyntaxDeclarationTestingData))]
    public void StateSyntaxDeclarationTest(DataUnit<string, StateSyntaxDeclaration> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Grammar.State, new StateSyntaxDeclarationComparer());
    }

    [Theory, ClassData(typeof(LambdaSyntaxDeclarationTestingData))]
    public void LambdaSyntaxDeclarationTest(DataUnit<string, LambdaSyntaxDeclaration> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Grammar.Lambda, new LambdaSyntaxDeclarationComparer());
    }

    [Theory, ClassData(typeof(RequireSyntaxDeclarationTestingData))]
    public void RequireSyntaxDeclarationTest(DataUnit<string, RequireSyntaxDeclaration> unit)
    {
        TestingHelpers.TestParsingDataUnit(unit, Grammar.Require, new RequireSyntaxDeclarationComparer());
    }

}
