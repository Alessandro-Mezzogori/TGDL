using System.Data;

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

    public static LiteralExpressionSyntax Literal(string value, TGDLType type)
    {
        return new LiteralExpressionSyntax(value, type);
    }

    public static AttributeSyntaxDeclaration StateAttribute(IdentifierSyntaxToken identifier, LiteralExpressionSyntax initializeValue)
    {
        return new AttributeSyntaxDeclaration(identifier, initializeValue);
    }

    public static StateScopeToken StateScope(StateScope scope)
    {
        return new StateScopeToken(scope);
    } 

    public static StateSyntaxDeclaration State(IdentifierSyntaxToken identifier, StateScopeToken? scope = null, IEnumerable<AttributeSyntaxDeclaration>? attributes = null)
    {
        scope ??= SyntaxFactory.StateScope(Syntax.StateScope.Local);
        attributes ??= Enumerable.Empty<AttributeSyntaxDeclaration>();
        return new StateSyntaxDeclaration(identifier, attributes, scope);
    }

    public static LambdaSyntaxDeclaration Lambda(BodySyntaxDeclaration body, IEnumerable<ParameterSyntaxDeclaration>? parameters = null)
    {
        parameters ??= Enumerable.Empty<ParameterSyntaxDeclaration>();
        return new LambdaSyntaxDeclaration(parameters, body);
    }

    public static ParameterSyntaxDeclaration Parameter(TypeSyntax type, IdentifierSyntaxToken identifier)
    {
        return new ParameterSyntaxDeclaration(type, identifier);
    }

    public static BodySyntaxDeclaration Body(IEnumerable<StatementSyntax>? statements = null)
    {
        statements ??= Enumerable.Empty<StatementSyntax>();
        return new BodySyntaxDeclaration(statements);
    }

    public static ReturnStatementSyntax Return(ExpressionSyntax expression)
    {
        return new ReturnStatementSyntax(expression);
    }

    public static BinaryOperationExpressionSyntax BinaryOperation(ExpressionSyntax left, ExpressionSyntax right, OperatorKind op)
    {
        return new BinaryOperationExpressionSyntax(left, op, right);
    }

    public static AttributeAccessExpressionSyntax AttributeAccess(IdentifierSyntaxToken target, IdentifierSyntaxToken attribute)
    {
        return new AttributeAccessExpressionSyntax(target, attribute);
    }

    public static LiteralExpressionSyntax Literal(string value, PredefinedTypeSyntax type)
    {
        return new LiteralExpressionSyntax(value, type);
    }
}
