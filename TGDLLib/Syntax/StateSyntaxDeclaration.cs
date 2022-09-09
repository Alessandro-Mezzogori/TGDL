namespace TGDLLib.Syntax;

public class StateSyntaxDeclaration
{
    public StateScopeToken Scope { get; }
    public IdentifierSyntaxToken Identifier { get; }
    public IEnumerable<AttributeSyntaxDeclaration> Attributes { get; }

    // StateActions

    public StateSyntaxDeclaration(IdentifierSyntaxToken identifier, IEnumerable<AttributeSyntaxDeclaration> attributes, StateScopeToken scope)
    {
        Identifier = identifier;
        Attributes = attributes;
        Scope = scope;
    }
}
