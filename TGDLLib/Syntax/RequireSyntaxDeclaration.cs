namespace TGDLLib.Syntax;

public class RequireSyntaxDeclaration
{
    // all require lambda need to be checked so that they return bool 
    public IEnumerable<LambdaSyntaxDeclaration> Requirements { get; } 

    public RequireSyntaxDeclaration(IEnumerable<LambdaSyntaxDeclaration> requirements)
    {
        Requirements = requirements;
    }
}
