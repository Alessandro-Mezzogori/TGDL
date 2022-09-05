namespace TGDLLib.Syntax;

public class LambdaExpressionDeclaration
{
    TypeSyntaxToken ReturnType { get; set; }
    IEnumerable<ParameterSyntaxDeclaration> Parameters { get; }
    BodySyntaxDeclaration Body { get; }

    public LambdaExpressionDeclaration(TypeSyntaxToken returnType, IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body)
    {
        ReturnType = returnType;
        Parameters = parameters;
        Body = body;
    }
}
