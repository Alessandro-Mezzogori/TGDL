namespace TGDLLib.Syntax;

public class LiteralExpressionSyntax : ExpressionSyntax
{
    public string Value { get; } 
    public TypeSyntax Type { get; }

    public LiteralExpressionSyntax(string value, TGDLType type)
    {
        Value = value;
        Type = SyntaxFactory.PredefinedType(type);
    }
}
