namespace TGDLLib.Syntax;

public class IdentifierSyntaxToken
{
    public string Identifier { get; set; }

    public IdentifierSyntaxToken(string identifier)
    {
        Identifier = identifier;
    }
}
