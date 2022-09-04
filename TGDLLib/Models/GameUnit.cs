namespace TGDLLib;

/// <summary>
/// Describes a game, is created 
/// </summary>
public class GameUnit
{
    List<Player> Players { get; set; } = new();
    State GlobalState { get; set; } = new();
    List<State> LocalStates { get; set; } = new();
    List<State> GroupStates { get; set; } = new();
}
