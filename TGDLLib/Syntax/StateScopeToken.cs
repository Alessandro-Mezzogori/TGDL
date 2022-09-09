namespace TGDLLib.Syntax;

public enum StateScope
{
    Local = 0, // Default
    Group,
    Global
}

public class StateScopeToken : IEquatable<StateScopeToken>
{
    public StateScope Scope { get; set; }

    public StateScopeToken(StateScope scope)
    {
        Scope = scope;
    }

    public bool Equals(StateScopeToken? other)
    {
        return other != null && other.Scope == Scope;
    }
}
