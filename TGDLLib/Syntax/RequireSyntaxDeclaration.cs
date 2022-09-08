namespace TGDLLib.Syntax;

public class RequireSyntaxDeclaration
{
    public IEnumerable<RequireLambdaSyntaxDeclaration> Expressions { get; }

    public RequireSyntaxDeclaration(IEnumerable<RequireLambdaSyntaxDeclaration> expressions)
    {
        Expressions = expressions;
    }
}
