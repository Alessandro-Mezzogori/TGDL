namespace TGDLLib.Syntax;

public class LiteralExpressionSyntax : ExpressionSyntax
{
    public string Value { get; } 
    public TypeSyntaxToken Type { get; }

    public LiteralExpressionSyntax(string value, TGDLType type)
    {
        Value = value;
        Type = new(type);
    }
}
