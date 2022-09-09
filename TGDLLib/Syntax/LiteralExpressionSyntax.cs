namespace TGDLLib.Syntax;

public class LiteralExpressionSyntax : ExpressionSyntax
{
    public string Value { get; } 
    public PredefinedTypeSyntax Type { get; } 

    public LiteralExpressionSyntax(string value, TGDLType type) // TODO Maybe remove
    {
        Value = value;
        Type = SyntaxFactory.PredefinedType(type);
    }

    public LiteralExpressionSyntax(string value, PredefinedTypeSyntax type)
    {
        Value = value;
        Type = type;
    }
}
