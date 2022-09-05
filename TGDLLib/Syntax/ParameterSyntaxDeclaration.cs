namespace TGDLLib.Syntax;

public class ParameterSyntaxDeclaration
{
    // Target, if null it will be passed by the caller ( not inferred from the target ), may be not neede

    // Type
    public TypeSyntaxToken Type { get; set; }

    // Identifier
    public IdentifierSyntaxToken Identifier { get; set; }

    
    public ParameterSyntaxDeclaration(TypeSyntaxToken type, IdentifierSyntaxToken identifier)
    {
        Type = type;
        Identifier = identifier;
    }
}
