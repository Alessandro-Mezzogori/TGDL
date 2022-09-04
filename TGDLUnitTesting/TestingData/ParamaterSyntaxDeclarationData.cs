using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    public class DataUnit<TInput, TOutput> 
        where TOutput : notnull
        where TInput : notnull
    {
        public TInput Input { get; set; }
        public TOutput Output { get; set; }
    }

    internal abstract class ClassDataList<TInput, TOutput> : IEnumerable<object[]>
        where TInput : notnull
        where TOutput : notnull
    {
        public abstract List<DataUnit<TInput, TOutput>> DataList { get; }
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return DataList.ToArray();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal abstract class ParserDataList<TOutput> : ClassDataList<string, TOutput>
        where TOutput : notnull
    {
    }

    // TODO BUG FIX, list is passed as 3 parameters instead of 1 at the time
    internal class ParameterSyntaxDeclarationData : ParserDataList<ParameterSyntaxDeclaration>
    {
        public override List<DataUnit<string, ParameterSyntaxDeclaration>> DataList => new()
        {
            new(){
                Input = "bool testBool",
                Output = new ParameterSyntaxDeclaration(
                    new TypeSyntaxToken("bool"),
                    new IdentifierSyntaxToken("testBool"))
            },
            new(){
                Input = "int testInt",
                Output = new ParameterSyntaxDeclaration(
                    new TypeSyntaxToken("bool"),
                    new IdentifierSyntaxToken("testInt"))
            },
            new(){
                Input = "string testString",
                Output = new ParameterSyntaxDeclaration(
                    new TypeSyntaxToken("string"),
                    new IdentifierSyntaxToken("testString"))
            },
        };
    }
}
