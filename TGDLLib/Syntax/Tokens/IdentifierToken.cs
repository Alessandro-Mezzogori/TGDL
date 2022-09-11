namespace TGDLLib.Syntax;

public class IdentifierToken : IEquatable<IdentifierToken>
{
    public string Identifier { get; set; }

    public IdentifierToken(string identifier)
    {
        Identifier = identifier;
    }

    public bool Equals(IdentifierToken? other)
    {
        return other != null && Identifier == other.Identifier;
    }
}
