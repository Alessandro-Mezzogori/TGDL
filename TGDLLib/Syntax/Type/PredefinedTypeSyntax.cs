namespace TGDLLib.Syntax;

public class PredefinedTypeSyntax : TypeSyntax, IEquatable<PredefinedTypeSyntax>
{
    // TypeToken for the predefined type aka void, decimal, string, bool
    public TGDLType Type { get; }
    public PredefinedTypeSyntax(TGDLType type)
    {
        IsPredifinedType = true;
        Type = type;
    }

    public bool Equals(PredefinedTypeSyntax? other)
    {
        return other != null && Type == other.Type;
    }
}

public class SuppliedPredefinedTypeSyntax : PredefinedTypeSyntax
{
    public readonly TGDLType[] AllowedTypes = new[] { TGDLType.Player, TGDLType.Board, TGDLType.BoardCell };
    public SuppliedPredefinedTypeSyntax(TGDLType type) : base(type)
    {
        if (!AllowedTypes.Contains(type))
            throw new ArgumentException($"{nameof(type)} {type} is not a supplied predefiend type");
    }
}


// sUpported types:
// strings for descriptinos
// decimal 
// void
// bool
// state
// string, decimal, void and bool are predefined or primitive types
// state is a declared type 
