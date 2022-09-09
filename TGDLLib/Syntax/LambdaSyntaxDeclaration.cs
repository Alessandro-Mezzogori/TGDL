namespace TGDLLib.Syntax;


// Return Type should be inferred by the body declaration return types 
// If require lambda and body syntax has no return bool -> throw error
// if action lambda and has different return from void -> throw error
public class LambdaSyntaxDeclaration
{
    public TypeSyntax ReturnType { get; }
    public IEnumerable<ParameterSyntaxDeclaration> Parameters { get; }
    public BodySyntaxDeclaration Body { get; }

    /// <summary>
    /// Returns type is inferred from body
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="body"></param>
    public LambdaSyntaxDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body)
    {
        Parameters = parameters;
        Body = body;
        ReturnType = SyntaxFactory.PredefinedType(TGDLType.Void); // Default return type
        var returnStaments = Body.Statements.OfType<ReturnStatementSyntax>();
            
        // All returns statements must have the same type
    }
}

public class RequireLambdaSyntaxDeclaration : LambdaSyntaxDeclaration
{
    public RequireLambdaSyntaxDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body) : 
        base(parameters, body)
    {
    }
}

public class ActionLambdaSyntaxDeclaration : LambdaSyntaxDeclaration
{
    public ActionLambdaSyntaxDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body) 
        : base(parameters, body)
    {
    }
}
