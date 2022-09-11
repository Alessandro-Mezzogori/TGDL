namespace TGDLLib.Syntax;

public class StateSyntaxDeclaration
{
    public StateScopeToken Scope { get; }
    public IdentifierToken Identifier { get; }
    public IEnumerable<AttributeSyntaxDeclaration> Attributes { get; }

    // StateActions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier">Name of the state</param>
    /// <param name="attributes">Attribute of the states, default is empty</param>
    /// <param name="scope">Scope of the state, default is <see cref="StateScope.Local"/></param>
    public StateSyntaxDeclaration(IdentifierToken identifier, IEnumerable<AttributeSyntaxDeclaration>? attributes = null, StateScopeToken? scope = null)
    {
        Identifier = identifier;
        Attributes = attributes ?? Enumerable.Empty<AttributeSyntaxDeclaration>();
        Scope = scope ?? SyntaxFactory.StateScope(StateScope.Local);
    }
}
