namespace TGDLLib.Syntax;

public class AttributeAccessExpressionSyntax : ExpressionSyntax
{
    public IdentifierSyntaxToken Target { get; } // Change to StateSyntax
    public IdentifierSyntaxToken Member { get; } // Change to AttributeSyntax

    public AttributeAccessExpressionSyntax(IdentifierSyntaxToken target, IdentifierSyntaxToken member)
    {
        Target = target;
        Member = member;
    }
}
