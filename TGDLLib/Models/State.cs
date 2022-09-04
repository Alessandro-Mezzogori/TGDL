namespace TGDLLib;

public class StateMetadata
{
    // scope modifiers
    // attached to who
}

public class State
{
    string Scope { get; set; } = "local";
    string Name { get; set; }

    Dictionary<string, object> Parameters { get; set; } = new();
    Dictionary<string, GameAction> Actions { get; set; } = new();

    public State(string scope, string name, Dictionary<string, object> parameters, Dictionary<string, GameAction> actions)
    {
        Scope = scope;
        Name = name;
        Parameters = parameters;
        Actions = actions;
    }

    public State(string name)
    {
        Name = name;
    }

    public State()
    {
        Name = ToString() ?? GetHashCode().ToString();
    }
}
