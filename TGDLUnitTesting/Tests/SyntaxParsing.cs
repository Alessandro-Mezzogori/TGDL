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
    }

    [Theory]
    [ClassData(typeof(ParametersSyntaxDeclarationData))]
    public void ParametersSyntaxDeclarationTest(DataUnit<string, IEnumerable<ParameterSyntaxDeclaration>> unit)
    {
    }

    [Theory, ClassData(typeof(BodySyntaxDeclarationTestingData))]
    public void BodySyntaxDeclarationTest(DataUnit<string, BodySyntaxDeclaration> unit)
    {
    }

    [Theory, ClassData(typeof(StateSyntaxDeclarationTestingData))]
    public void StateSyntaxDeclarationTest(DataUnit<string, StateSyntaxDeclaration> unit)
    {
    }

    [Theory, ClassData(typeof(LambdaSyntaxDeclarationTestingData))]
    public void LambdaSyntaxDeclarationTest(DataUnit<string, LambdaSyntaxDeclaration> unit)
    {
    }

    //[Theory, ClassData(typeof(RequireSyntaxDeclarationTestingData))]
    public void RequireSyntaxDeclarationTest(DataUnit<string, RequireSyntaxDeclaration> unit)
    {
    }

}
