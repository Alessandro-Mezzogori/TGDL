namespace TGDLLib.Syntax;

public class DeclaredTypeSyntax : TypeSyntax, IEquatable<DeclaredTypeSyntax>
{
    public IdentifierToken DeclaredType { get; }
    public DeclaredTypeSyntax(IdentifierToken declaredType)
    {
        IsDeclaredType = true;
        DeclaredType = declaredType;
    }

    public bool Equals(DeclaredTypeSyntax? other)
    {
        return other != null && DeclaredType.Equals(other.DeclaredType);
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
