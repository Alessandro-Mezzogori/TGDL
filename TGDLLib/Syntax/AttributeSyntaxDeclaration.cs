using System.Diagnostics.CodeAnalysis;

namespace TGDLLib.Syntax;

public class AttributeSyntaxDeclaration
{
    // type is derived from initializing value
    public IdentifierToken Identifier { get; }
    public LiteralExpressionSyntax InitializingValue { get; }  // TODO Support for initialiazation with attribute access
    public TGDLType Type => InitializingValue.Type.Type;

    public AttributeSyntaxDeclaration(IdentifierToken identifier, LiteralExpressionSyntax initializingValue)
    {
        Identifier = identifier;
        InitializingValue = initializingValue;
    }
}
