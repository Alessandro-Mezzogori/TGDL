namespace TGDLLib.Syntax;

public class GameActionSyntaxDeclaration
{
    public IdentifierSyntaxToken Identifier;
    // Require Func<bool, something>
    public RequireSyntaxDeclaration? Require { get; }

    // Triggers can be anything may be the most difficult to get right
    // Delegate Func<void, something>

    public GameActionSyntaxDeclaration(IdentifierSyntaxToken identifier, RequireSyntaxDeclaration? require = null)
    {
        Identifier = identifier;
        Require = require;
    }
}

public class RequireSyntaxDeclaration
{
    public IEnumerable<RequireLambdaExpressionDeclaration> Expressions { get; }

    public RequireSyntaxDeclaration(IEnumerable<RequireLambdaExpressionDeclaration> expressions)
    {
        Expressions = expressions;
    }
}

public class FunctionSyntaxDeclaration
{
    IdentifierSyntaxToken? Identifier { get; set; } = null;
    // Return Type
    TypeSyntaxToken ReturnType { get; set; }
    // Parameters 
    List<ParameterSyntaxDeclaration> Parameters { get; set; }

    // Body
    BodySyntaxDeclaration Body { get; set; }

    public FunctionSyntaxDeclaration(IdentifierSyntaxToken identifier, TypeSyntaxToken returnType, List<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body)
    {
        Identifier = identifier;
        ReturnType = returnType;
        Parameters = parameters;
        Body = body;
    }
}

public class LambdaExpressionDeclaration
{
    TypeSyntaxToken ReturnType { get; set; }
    IEnumerable<ParameterSyntaxDeclaration> Parameters { get; }
    BodySyntaxDeclaration Body { get; }

    public LambdaExpressionDeclaration(TypeSyntaxToken returnType, IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body)
    {
        ReturnType = returnType;
        Parameters = parameters;
        Body = body;
    }
}

public class RequireLambdaExpressionDeclaration : LambdaExpressionDeclaration
{
    public static readonly TypeSyntaxToken RequireLambdaReturnType = new TypeSyntaxToken("bool"); 
    public RequireLambdaExpressionDeclaration(IEnumerable<ParameterSyntaxDeclaration> parameters, BodySyntaxDeclaration body) 
        : base(RequireLambdaReturnType, parameters, body) { }
}

public class ParameterSyntaxDeclaration
{
    // Target, if null it will be passed by the caller ( not inferred from the target ), may be not neede

    // Type
    public TypeSyntaxToken Type { get; set; }

    // Identifier
    public IdentifierSyntaxToken Identifier { get; set; }

    
    public ParameterSyntaxDeclaration(TypeSyntaxToken type, IdentifierSyntaxToken identifier)
    {
        Type = type;
        Identifier = identifier;
    }
}

public class BodySyntaxDeclaration
{
    
    // List of Statements that can be:
    // - Assignment
    // - Operation: boolean or mathematical
    // - Conditional
    // - Return Statement
}

public class TargetSyntaxDeclaration
{
    // needs to target a specific instance or instancens of a type
}

public class TypeSyntaxToken
{
    public string Type { get; set; }

    public TypeSyntaxToken(string type)
    {
        Type = type;
    }
}

public class IdentifierSyntaxToken
{
    public string Identifier { get; set; }

    public IdentifierSyntaxToken(string identifier)
    {
        Identifier = identifier;
    }
}

public static class TokenConstants
{
    public const char ParameterDelimiterToken = ',';
    public const string LambdaBodyDelimiter = "=>";

    public const char BlockStart = ':';
    public const string RequireToken = "require";
    public const string ActionToken = "action";
}
