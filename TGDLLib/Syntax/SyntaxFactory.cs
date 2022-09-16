using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using TGDLLib.Syntax.Expressions;
using TGDLLib.Syntax.Statements;

namespace TGDLLib.Syntax;

public class SyntaxFactory
{
    public static IdentifierToken Identifier(string identifier)
    {
        return new IdentifierToken(identifier);
    }

    public static IdentifierNameExpressionSyntax IdentifierName(IdentifierToken identifier)
    {
        return new IdentifierNameExpressionSyntax(identifier);
    }

    public static IdentifierNameExpressionSyntax IdentifierName(string identifier)
    {
        return IdentifierName(Identifier(identifier));
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

    public static DeclaredTypeSyntax DeclaredType(IdentifierToken declaredType)
    {
        return new DeclaredTypeSyntax(declaredType);
    }

    public static DeclaredTypeSyntax DeclaredType(string declaredType)
    {
        return DeclaredType(Identifier(declaredType));
    }

    public static TypeDeclarationSyntax TypeDeclaration(TGDLType type, IdentifierToken typeIdenfitier)
    {
        return new TypeDeclarationSyntax(type, typeIdenfitier);
    }

    public static TypeDeclarationSyntax StateTypeDeclaration(IdentifierToken typeIdentifier)
    {
        return TypeDeclaration(TGDLType.State, typeIdentifier);
    }

    public static LiteralExpressionSyntax Literal(string value, TGDLType type)
    {
        return new LiteralExpressionSyntax(value, type);
    }

    public static UnaryExpressionSyntax Unary(ExpressionSyntax operand, OperationKind operation)
    {
        return new UnaryExpressionSyntax(operand, operation);
    }

    public static StateScopeToken StateScope(StateScope scope)
    {
        return new StateScopeToken(scope);
    } 

    public static StateSyntaxDeclaration State(IdentifierToken identifier, StateScopeToken? scope = null, IEnumerable<AssignmentStatementSyntax>? attributes = null)
    {
        scope ??= SyntaxFactory.StateScope(Syntax.StateScope.Local);
        attributes ??= Enumerable.Empty<AssignmentStatementSyntax>();
        return new StateSyntaxDeclaration(identifier, attributes, scope);
    }

    public static ExpressionStatement Expression(ExpressionSyntax expression)
    {
        return new ExpressionStatement(expression);
    }

    public static AssignmentStatementSyntax StateAttribute(IdentifierNameExpressionSyntax identifier, ExpressionSyntax initializer)
    {
        return Assignment(identifier, initializer);
    }

    public static AssignmentStatementSyntax StateAttribute(string identifier, ExpressionSyntax initializer)
    {
        return StateAttribute(IdentifierName(identifier), initializer);
    }


    public static LambdaSyntaxDeclaration Lambda(BodySyntaxDeclaration body, IEnumerable<ParameterSyntaxDeclaration>? parameters = null)
    {
        parameters ??= Enumerable.Empty<ParameterSyntaxDeclaration>();
        return new LambdaSyntaxDeclaration(parameters, body);
    }

    public static ParameterSyntaxDeclaration Parameter(TypeSyntax type, IdentifierToken identifier)
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

    public static AssignmentStatementSyntax Assignment(ExpressionSyntax identifier, ExpressionSyntax initializer)
    {
        return new AssignmentStatementSyntax(identifier, initializer);
    }

    public static BinaryOperationExpressionSyntax BinaryOperation(ExpressionSyntax left, ExpressionSyntax right, OperationKind op)
    {
        return new BinaryOperationExpressionSyntax(left, op, right);
    }

    public static LiteralExpressionSyntax Literal(string value, PredefinedTypeSyntax type)
    {
        return new LiteralExpressionSyntax(value, type);
    }
}
