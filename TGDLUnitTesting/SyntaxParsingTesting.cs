using Sprache;
using TGDLLib;
using TGDLLib.Syntax;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting
{
    public class SyntaxParsingTesting
    {
        [Theory]
        [InlineData(" id", "id", false)]
        [InlineData("\"id", "id", true)]
        public void IdentifierTokenTest(string input, string expected, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ParseException>(() => Parsers.IdentifierToken.Parse(input));
            } 
        }

        [Theory]
        [InlineData(" id", "id", false)]
        [InlineData("\"id", "id", true)]
        public void TypeTokenTest(string input, string expected, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ParseException>(() => Parsers.TypeToken.Parse(input));
            }
        }

        [Theory]
        [ClassData(typeof(ParameterSyntaxDeclarationData))] 
        public void ParamaterSyntaxDeclarationTest(DataUnit<string, ParameterSyntaxDeclaration> unit)
        {
            var parsed = Parsers.ParameterSyntax.Parse(unit.Input);

            Assert.Equal(parsed.Type, unit.Output.Type);
            Assert.Equal(parsed.Identifier, unit.Output.Identifier);
        }

    }
}
