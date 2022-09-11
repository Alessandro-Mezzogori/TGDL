namespace TGDLLib.Syntax;

public class ParameterSyntaxDeclaration
{
    // Target, if null it will be passed by the caller ( not inferred from the target ), may be not neede

    // Type
    public TypeSyntax Type { get; set; }

    // Identifier
    public IdentifierToken Identifier { get; set; }

    
    public ParameterSyntaxDeclaration(TypeSyntax type, IdentifierToken identifier)
    {
        Type = type;
        Identifier = identifier;
    }
}
