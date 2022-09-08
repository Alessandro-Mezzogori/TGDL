namespace TGDLLib.Syntax;


// Return Type should be inferred by the body declaration return types 
// If require lambda and body syntax has no return bool -> throw error
// if action lambda and has different return from void -> throw error
public class LambdaSyntaxDeclaration
{
    public TypeSyntaxToken ReturnType { get; }
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
        ReturnType = new TypeSyntaxToken(TGDLType.Void); // Default return type
        var returnStaments = Body.Statements.OfType<ReturnStatementSyntax>();
            
        // All returns statements must have the same type
    }
}

public class RequireLambdaSyntaxDeclaration : LambdaSyntaxDeclaration
{
    private static readonly TypeSyntaxToken _expectedReturnType = new(TGDLType.Bool);

    public RequireLambdaSyntaxDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body) : 
        base(parameters, body)
    {
    }
}

public class ActionLambdaSyntaxDeclaration : LambdaSyntaxDeclaration
{
    private static readonly TypeSyntaxToken _expectedReturnType = new(TGDLType.Void);

    public ActionLambdaSyntaxDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body) 
        : base(parameters, body)
    {
    }
}
