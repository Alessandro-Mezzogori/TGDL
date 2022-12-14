namespace TGDLLib.Syntax;

public class GameActionSyntaxDeclaration
{
    public IdentifierToken Identifier;
    // Require Func<bool, something>
    public RequireSyntaxDeclaration? Require { get; }

    // Triggers can be anything may be the most difficult to get right
    // Delegate Func<void, something>

    public GameActionSyntaxDeclaration(IdentifierToken identifier, RequireSyntaxDeclaration? require = null)
    {
        Identifier = identifier;
        Require = require;
    }
}
