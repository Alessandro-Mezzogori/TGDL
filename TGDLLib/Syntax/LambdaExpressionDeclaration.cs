namespace TGDLLib.Syntax;


// Return Type should be inferred by the body declaration return types 
// If require lambda and body syntax has no return bool -> throw error
// if action lambda and has different return from void -> throw error
public class LambdaSyntaxDeclaration
{
    TypeSyntaxToken ReturnType { get; set; }
    IEnumerable<ParameterSyntaxDeclaration> Parameters { get; }
    BodySyntaxDeclaration Body { get; }

    public LambdaSyntaxDeclaration(TypeSyntaxToken returnType, IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body)
    {
        ReturnType = returnType;
        Parameters = parameters;
        Body = body;
    }
}

public class RequireLambdaSyntaxDeclaration : LambdaSyntaxDeclaration
{
    public RequireLambdaSyntaxDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body) : 
        base(new TypeSyntaxToken("bool"), parameters, body)
    {
    }
}

public class ActionLambdaSyntaxDeclaration : LambdaSyntaxDeclaration
{
    public ActionLambdaSyntaxDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body) 
        : base(new TypeSyntaxToken("Void"), parameters, body)
    {
    }
}
