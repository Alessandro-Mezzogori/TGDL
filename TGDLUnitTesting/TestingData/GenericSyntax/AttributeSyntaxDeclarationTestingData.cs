using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData.GenericSyntax
{
    internal class AttributeSyntaxDeclarationTestingData : ParserDataList<AttributeSyntaxDeclaration>
    {
        public override List<DataUnit<string, AttributeSyntaxDeclaration>> DataList => new()
        {
            new()
            {
                Input = "a = 1",
                Output = sf.StateAttribute(
                    sf.Identifier("a"),
                    sf.Literal("1", TGDLType.Decimal)
                )            
            },
            new()
            {
                Input = "a= 1",
                Output = sf.StateAttribute(
                    sf.Identifier("a"),
                    sf.Literal("1", TGDLType.Decimal)
                )            
            },
            new()
            {
                Input = "a=1",
                Output = sf.StateAttribute(
                    sf.Identifier("a"),
                    sf.Literal("1", TGDLType.Decimal)
                )            
            },
            new()
            {
                Input = "a = \"test\"",
                Output = sf.StateAttribute(
                    sf.Identifier("a"),
                    sf.Literal("test", TGDLType.String)
                )            
            },
            new()
            {
                Input = "a = true",
                Output = sf.StateAttribute(
                    sf.Identifier("a"),
                    sf.Literal("true", TGDLType.Bool)
                )            
            },
            /*
            Full expression initialization support for state attributes
            new()
            {
                Input = "a = this.access",
                Output = sf.StateAttribute(
                    sf.Identifier("a"),
                    sf.Literal("1", TGDLType.Decimal)
                )            
            },
            new()
            {
                Input = "a = 1",
                Output = sf.StateAttribute(
                    sf.Identifier("a"),
                    sf.Literal("1", TGDLType.Decimal)
                )            
            },
            new()
            {
                Input = "a = 1",
                Output = sf.StateAttribute(
                    sf.Identifier("a"),
                    sf.Literal("1", TGDLType.Decimal)
                )            
            },
            */
        };
    }

    internal class AttributeSyntaxDeclarationComparer : IEqualityComparer<AttributeSyntaxDeclaration>
    {
        private readonly LiteralExpressionSyntaxComparer _literalComparer = new(); 
        public bool Equals(AttributeSyntaxDeclaration? x, AttributeSyntaxDeclaration? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return _literalComparer.Equals(x.InitializingValue, y.InitializingValue) 
                    && x.Identifier.Equals(y.Identifier);
        }

        public int GetHashCode([DisallowNull] AttributeSyntaxDeclaration obj)
        {
            return obj.GetHashCode();
        }
    }
}
