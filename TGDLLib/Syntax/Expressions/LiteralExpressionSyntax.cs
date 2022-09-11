namespace TGDLLib.Syntax;

public class LiteralExpressionSyntax : ExpressionSyntax
{
    public string Value { get; } 
    public PredefinedTypeSyntax Type { get; } 


    public LiteralExpressionSyntax(string value, PredefinedTypeSyntax type)
        : base(OperationKind.Literal, type.Type)
    {
        Value = value;
        Type = type;
    }
    public LiteralExpressionSyntax(string value, TGDLType type) // TODO Maybe remove
        : this(value, SyntaxFactory.PredefinedType(type))
    {
    }
}
