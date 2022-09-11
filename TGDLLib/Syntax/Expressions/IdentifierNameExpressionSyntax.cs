namespace TGDLLib.Syntax.Expressions;

public class IdentifierNameExpressionSyntax : ExpressionSyntax, IEquatable<IdentifierNameExpressionSyntax>
{
    public IdentifierToken IdentifierToken { get; }

    public IdentifierNameExpressionSyntax(IdentifierToken identifierToken) : base(OperationKind.Identifier)
    {
        IdentifierToken = identifierToken;
    }

    public bool Equals(IdentifierNameExpressionSyntax? other)
    {
        if (other == null) return false;

        return IdentifierToken.Equals(other.IdentifierToken);
    }
}
