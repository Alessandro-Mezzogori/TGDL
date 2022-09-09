using System.Diagnostics.CodeAnalysis;

namespace TGDLLib.Syntax;

public class AttributeSyntaxDeclaration
{
    // type is derived from initializing value
    public IdentifierSyntaxToken Identifier { get; }
    public LiteralExpressionSyntax InitializingValue { get; }  // TODO Support for initialiazation with attribute access

    public AttributeSyntaxDeclaration(IdentifierSyntaxToken identifier, LiteralExpressionSyntax initializingValue)
    {
        Identifier = identifier;
        InitializingValue = initializingValue;
    }
}
