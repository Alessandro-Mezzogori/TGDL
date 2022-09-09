namespace TGDLLib.Syntax;

public class SyntaxFactory
{
    public static IdentifierSyntaxToken Identifier(string identifier)
    {
        return new IdentifierSyntaxToken(identifier);
    }

    public static TypeSyntax ParseTypeName(string type)
    {
        // TODO put parsing for type in predefined type
        if (type == "string") return PredefinedType(TGDLType.String);
        if (type == "decimal") return PredefinedType(TGDLType.Decimal);
        if (type == "bool") return PredefinedType(TGDLType.Bool);
        if (type == "state") return SuppliedPredefinedType(TGDLType.State);
        if (type == "player") return SuppliedPredefinedType(TGDLType.Player);
        if (type == "board") return SuppliedPredefinedType(TGDLType.Board);
        if (type == "boardCell") return SuppliedPredefinedType(TGDLType.BoardCell);

        throw new ArgumentException($"{type} is not a predefined type or suppplied predefined type");
    }

    public static PredefinedTypeSyntax PredefinedType(TGDLType type)
    {
        return new PredefinedTypeSyntax(type);
    }

    public static PredefinedTypeSyntax PredefinedType(string type)
    {
        // TODO Put in reserved keyword class 
        if (type == "string") return PredefinedType(TGDLType.String);
        if (type == "decimal") return PredefinedType(TGDLType.Decimal);
        if (type == "bool") return PredefinedType(TGDLType.Bool);

        throw new ArgumentException($"{nameof(type)}: {type} is not a predefined type"); 
    }

    public static SuppliedPredefinedTypeSyntax SuppliedPredefinedType(TGDLType type)
    {
        return new SuppliedPredefinedTypeSyntax(type);
    }

    public static SuppliedPredefinedTypeSyntax SuppliedPredfinedType(string type)
    {
        if (type == "state") return SuppliedPredefinedType(TGDLType.State);
        if (type == "player") return SuppliedPredefinedType(TGDLType.Player);
        if (type == "board") return SuppliedPredefinedType(TGDLType.Board);
        if (type == "boardCell") return SuppliedPredefinedType(TGDLType.BoardCell);

        throw new ArgumentException($"{nameof(type)}: {type} is not a supplied predefined type"); 
    }

    public static DeclaredTypeSyntax DeclaredType(TypeDeclarationSyntax type)
    {
        return new DeclaredTypeSyntax(type);
    }

    public static TypeDeclarationSyntax TypeDeclaration(TGDLType type, IdentifierSyntaxToken typeIdenfitier)
    {
        return new TypeDeclarationSyntax(type, typeIdenfitier);
    }

    public static TypeDeclarationSyntax StateTypeDeclaration(IdentifierSyntaxToken typeIdentifier)
    {
        return TypeDeclaration(TGDLType.State, typeIdentifier);
    }

    public static void ParseLiteralExpressino(string literal)
    {
        // number
        // 
    }
}
