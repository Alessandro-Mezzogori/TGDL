namespace TGDLLib.Syntax;

public class TypeDeclarationSyntax : IEquatable<TypeDeclarationSyntax>
{
    // identificatore per il tipo di oggetto dichiarato
    // identificatore per il nome dell'oggeto dichiarato
    public TGDLType Type { get; }
    public IdentifierSyntaxToken TypeIdentifier { get; }

    public TypeDeclarationSyntax(TGDLType type, IdentifierSyntaxToken typeIdentifier)
    {
        Type = type;
        TypeIdentifier = TypeIdentifier;
    }

    public bool Equals(TypeDeclarationSyntax? other)
    {
        return other != null && Type == other.Type && TypeIdentifier.Equals(other.TypeIdentifier);
    }
}

public class StateTypeDeclarationSyntax : TypeDeclarationSyntax
{
    public StateTypeDeclarationSyntax(IdentifierSyntaxToken typeIdentifier) 
        : base(TGDLType.State, typeIdentifier)
    {
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
