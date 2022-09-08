using Sprache;

namespace TGDLLib.Syntax;

public enum TGDLType
{
    Decimal,
    String,
    Bool,
    Void
}

public static class TypeExtensions
{
    public static string ToStringType(this TGDLType type)
    {
        return Enum.GetName(type) ?? throw new InvalidOperationException($"{type} string not found");
    }

    public static TGDLType ParseType(string type)
    {
        return Enum.Parse<TGDLType>(type);
    }
}

public class TypeSyntaxToken
{
    public TGDLType Type { get; set; }

    public TypeSyntaxToken(TGDLType type)
    {
        Type = type;
    }
}
