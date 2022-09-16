using TGDLLib;
using TGDLLib.Syntax;
using TGDLLib.Visitors;
using TGDLUnitTesting.TestingData;
using TGDLUnitTesting.TestingData.GenericSyntax;

namespace TGDLUnitTesting;


public class SyntaxParsing
{ 
    [Theory]
    [ClassData(typeof(ParameterSyntaxDeclarationData))]
    public void ParameterSyntaxDeclarationTest(DataUnit<string, ParameterSyntaxDeclaration> unit)
    {
        ParseTests.Test(unit, parser => parser.parameter(), tree => new ParameterVisitor().Visit(tree), new ParameterSyntaxDeclarationComparer());
    }

    [Theory, ClassData(typeof(BodySyntaxDeclarationTestingData))]
    public void BodySyntaxDeclarationTest(DataUnit<string, BodySyntaxDeclaration> unit)
    {
        ParseTests.Test(unit, parser => parser.body(), tree => new BodyVisitor().Visit(tree), new BodySyntaxDeclarationComparer());
    }

    [Theory, ClassData(typeof(StateSyntaxDeclarationTestingData))]
    public void StateSyntaxDeclarationTest(DataUnit<string, StateSyntaxDeclaration> unit)
    {
        Assert.Fail("Not Implemented");
    }

    [Theory, ClassData(typeof(LambdaSyntaxDeclarationTestingData))]
    public void LambdaSyntaxDeclarationTest(DataUnit<string, LambdaSyntaxDeclaration> unit)
    {
        ParseTests.Test(unit, parser => parser.lambda(), tree => new LambdaVisitor().Visit(tree), new LambdaSyntaxDeclarationComparer());
    }

    //[Theory, ClassData(typeof(RequireSyntaxDeclarationTestingData))]
    public void RequireSyntaxDeclarationTest(DataUnit<string, RequireSyntaxDeclaration> unit)
    {
        Assert.Fail("Not Implemented");
    }

}
