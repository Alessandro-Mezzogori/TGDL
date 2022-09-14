using TGDLLib.Syntax.Statements;

namespace TGDLLib.Syntax;

public class StateSyntaxDeclaration
{
    public StateScopeToken Scope { get; }
    public IdentifierToken Identifier { get; }
    public IEnumerable<AssignmentStatementSyntax> Attributes { get; }

    // StateActions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier">Name of the state</param>
    /// <param name="attributes">Attribute of the states, default is empty</param>
    /// <param name="scope">Scope of the state, default is <see cref="StateScope.Local"/></param>
    public StateSyntaxDeclaration(IdentifierToken identifier, IEnumerable<AssignmentStatementSyntax>? attributes = null, StateScopeToken? scope = null)
    {
        Identifier = identifier;
        Attributes = attributes ?? Enumerable.Empty<AssignmentStatementSyntax>();
        Scope = scope ?? SyntaxFactory.StateScope(StateScope.Local);
    }
}
