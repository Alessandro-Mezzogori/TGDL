namespace TGDLLib.Syntax;

public class RequireSyntaxDeclaration
{
    public IEnumerable<RequireLambdaExpressionDeclaration> Expressions { get; }

    public RequireSyntaxDeclaration(IEnumerable<RequireLambdaExpressionDeclaration> expressions)
    {
        Expressions = expressions;
    }
}
