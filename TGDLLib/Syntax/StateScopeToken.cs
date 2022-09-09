namespace TGDLLib.Syntax;

public enum StateScope
{
    Local,
    Group,
    Global
}

public class StateScopeToken
{
    public StateScope Scope { get; set; }

    public StateScopeToken(StateScope scope)
    {
        Scope = scope;
    }
}
