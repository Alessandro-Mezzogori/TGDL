using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Statements;
using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData.GenericSyntax;

internal class StateSyntaxDeclarationTestingData : ParserDataList<StateSyntaxDeclaration>
{
    public override List<DataUnit<string, StateSyntaxDeclaration>> DataList => new()
    {
        new()
        {
            Input = @"state test",
            Output = sf.State(
                sf.Identifier("test")
            )
        },
        new()
        {
            Input = @"local state test",
            Output = sf.State(
                sf.Identifier("test"),
                scope: sf.StateScope(StateScope.Local)
            )
        },
        new()
        {
            Input = @"group state test",
            Output = sf.State(
                sf.Identifier("test"),
                scope: sf.StateScope(StateScope.Group)
            )
        },
        new()
        {
            Input = @"global state test",
            Output = sf.State(
                sf.Identifier("test"),
                scope: sf.StateScope(StateScope.Global)
            )
        },
        new()
        {
            Input = "state test:\r\n\t attr = 1\r\n",
            Output = sf.State(
                sf.Identifier("test"),
                attributes: new AssignmentStatementSyntax[]
                {
                    sf.StateAttribute(
                        sf.IdentifierName("attr"), 
                        sf.Literal("1", TGDLType.Decimal)
                    )
                }
            )
        },
        new()
        {
            Input = "state test:\r\n\tattr = 1\r\n second = true\r\n",
            Output = sf.State(
                sf.Identifier("test"),
                attributes: new AssignmentStatementSyntax[]
                {
                    sf.StateAttribute(
                        sf.IdentifierName("attr"), 
                        sf.Literal("1", TGDLType.Decimal)
                    ),
                    sf.StateAttribute(
                        sf.IdentifierName("second"), 
                        sf.Literal("true", TGDLType.Bool)
                    )
                }
            )
        },
        new()
        {
            Input = "state test:\r\n\tattr = 1\r\n\tsecond=true",
            Output = sf.State(
                sf.Identifier("test"),
                attributes: new AssignmentStatementSyntax[]
                {
                    sf.StateAttribute(
                        sf.IdentifierName("attr"), 
                        sf.Literal("1", TGDLType.Decimal)
                    ),
                    sf.StateAttribute(
                        sf.IdentifierName("second"), 
                        sf.Literal("true", TGDLType.Bool)
                    )
                }
            )
        },
        new()
        {
             Input = "state test:\r\n\tattr = \"test\"\r\n\tsecond=true\r\n\tthird=false",
            Output = sf.State(
                sf.Identifier("test"),
                attributes: new AssignmentStatementSyntax[]
                {
                    sf.StateAttribute(
                        sf.IdentifierName("attr"), 
                        sf.Literal("test", TGDLType.String)
                    ),
                    sf.StateAttribute(
                        sf.IdentifierName("second"), 
                        sf.Literal("true", TGDLType.Bool)
                    ),
                    sf.StateAttribute(
                        sf.IdentifierName("third"), 
                        sf.Literal("false", TGDLType.Bool)
                    )

                }
            )
        },
    };
}

internal class StateSyntaxDeclarationComparer : IEqualityComparer<StateSyntaxDeclaration>
{
    private readonly AssignmentStatementSyntaxComparer _attributeComparer = new();
    public bool Equals(StateSyntaxDeclaration? x, StateSyntaxDeclaration? y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;

        return x.Identifier.Equals(y.Identifier)
            && x.Scope.Equals(y.Scope)
            && x.Attributes.SequenceEqual(y.Attributes, _attributeComparer); 
    }

    public int GetHashCode([DisallowNull] StateSyntaxDeclaration obj)
    {
        return obj.GetHashCode();
    }
}
