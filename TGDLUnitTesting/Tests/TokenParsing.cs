using Sprache;
using TGDLLib;

namespace TGDLUnitTesting
{
    public class TokenParsing
    {
        [Theory]
        [InlineData(" id", "id", false)]
        [InlineData("\"id", "id", true)]
        public void IdentifierTokenTest(string input, string expected, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ParseException>(() => Parsers.Tokens.Identifier.Parse(input));
            } 
        }

        [Theory]
        [InlineData(" id", "id", false)]
        [InlineData("\"id", "id", true)]
        public void TypeTokenTest(string input, string expected, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ParseException>(() => Parsers.Tokens.Type.Parse(input));
            }
        }

    }
}
