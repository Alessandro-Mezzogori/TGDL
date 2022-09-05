namespace TGDLLib.Syntax;

public class RequireLambdaExpressionDeclaration : LambdaExpressionDeclaration
{
    public static readonly TypeSyntaxToken RequireLambdaReturnType = new TypeSyntaxToken("bool"); 
    public RequireLambdaExpressionDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body) 
        : base(RequireLambdaReturnType, parameters, body) { }
}
