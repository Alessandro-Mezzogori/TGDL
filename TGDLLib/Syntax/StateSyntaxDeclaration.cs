namespace TGDLLib.Syntax;

public class StateSyntaxDeclaration
{
    // state scope modifier
    public IdentifierSyntaxToken Identifier { get; }
    public IEnumerable<AttributeSyntaxDeclaration> Attributes { get; }

    // StateActions

    public StateSyntaxDeclaration(IdentifierSyntaxToken identifier, IEnumerable<AttributeSyntaxDeclaration> attributes)
    {
        Identifier = identifier;
        Attributes = attributes;
    }
}
