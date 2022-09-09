namespace TGDLLib.Syntax;

public enum TGDLType
{
    Decimal,
    Bool,
    Void,
    String,
    State,
    Player,
    Board,
    BoardCell
}


public abstract class TypeSyntax
{
    public bool IsDeclaredType { get; protected set; } = false;
    public bool IsPredifinedType { get; protected set; } = false;
    public TypeSyntax()
    {
    }
}
