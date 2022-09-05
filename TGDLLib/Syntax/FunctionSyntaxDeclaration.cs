namespace TGDLLib.Syntax;

public class FunctionSyntaxDeclaration
{
    IdentifierSyntaxToken? Identifier { get; set; } = null;
    // Return Type
    TypeSyntaxToken ReturnType { get; set; }
    // Parameters 
    List<MemberAccessExpressionSyntax> Parameters { get; set; }

    // Body
    BodySyntaxDeclaration Body { get; set; }

    public FunctionSyntaxDeclaration(IdentifierSyntaxToken identifier, TypeSyntaxToken returnType, List<MemberAccessExpressionSyntax> parameters, BodySyntaxDeclaration body)
    {
        Identifier = identifier;
        ReturnType = returnType;
        Parameters = parameters;
        Body = body;
    }
}
