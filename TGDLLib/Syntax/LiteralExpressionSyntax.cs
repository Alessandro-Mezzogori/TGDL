namespace TGDLLib.Syntax;

public enum LiteralType
{
    Integer,
    Double,
}

public class LiteralExpressionSyntax : ExpressionSyntax
{
    public string Value { get; } 
    public LiteralType Type { get; }

    public LiteralExpressionSyntax(string value, LiteralType type)
    {
        Value = value;
        Type = type;
    }
}
