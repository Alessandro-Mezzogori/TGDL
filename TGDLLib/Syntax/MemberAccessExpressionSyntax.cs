namespace TGDLLib.Syntax;

public class MemberAccessExpressionSyntax : ExpressionSyntax
{
    public IdentifierSyntaxToken Target { get; }
    public IdentifierSyntaxToken Member { get; }

    public MemberAccessExpressionSyntax(IdentifierSyntaxToken target, IdentifierSyntaxToken member)
    {
        Target = target;
        Member = member;
    }
}
