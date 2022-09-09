namespace TGDLLib.Syntax;

public class IdentifierSyntaxToken : IEquatable<IdentifierSyntaxToken>
{
    public string Identifier { get; set; }

    public IdentifierSyntaxToken(string identifier)
    {
        Identifier = identifier;
    }

    public bool Equals(IdentifierSyntaxToken? other)
    {
        return other != null && Identifier == other.Identifier;
    }
}
