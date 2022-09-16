using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Statements;
using TGDLUnitTesting.TestingData;

namespace TGDLUnitTesting;

using sf = SyntaxFactory;

internal class BodySyntaxDeclarationTestingData : ParserDataList<BodySyntaxDeclaration>
{
    public override List<DataUnit<string, BodySyntaxDeclaration>> DataList => new()
    {
        new()
        {
            Input = "=> return 1 + 1",
            Output = sf.Body(new StatementSyntax[]
            {
                sf.Return(
                    sf.BinaryOperation(
                        sf.Literal("1", TGDLType.Decimal),
                        sf.Literal("1", TGDLType.Decimal),
                        OperationKind.Addition
                    )
                )
            })
        },
        new()
        {
            Input = @"=>
                        ",
            Output = sf.Body(new StatementSyntax[0]),
            Test = TestType.Fail
        },
        new() // Edge case, should i allow it ? 
        {
            Input = @"=>",
            Output = new(new StatementSyntax[0]),
            Test = TestType.Fail
        },
        new() // Edge case, should i allow it ? 
        {
            Input = "=>\r\nreturn 1 + 1",
            Output = new(new StatementSyntax[]
            {
                new ReturnStatementSyntax(
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperationKind.Addition,
                        new LiteralExpressionSyntax("1", TGDLType.Decimal)
                    )
                )
            })
        },
        new() // Edge case, should i allow it ? 
        {
            Input = "=> 1 + 1",
            Output = new(new StatementSyntax[]{
                new ExpressionStatement(
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperationKind.Addition,
                        new LiteralExpressionSyntax("1", TGDLType.Decimal)
                    )
                )
            })
        }

    };
}

internal class BodySyntaxDeclarationComparer : IEqualityComparer<BodySyntaxDeclaration>
{
    private readonly StatementSyntaxComparer _comparer = new();

    public bool Equals(BodySyntaxDeclaration? x, BodySyntaxDeclaration? y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;

        if (x.Statements.Count() != y.Statements.Count()) return false;


        return x.Statements.All(x => y.Statements.Contains(x, _comparer));
    }

    public int GetHashCode([DisallowNull] BodySyntaxDeclaration obj)
    {
        return obj.GetHashCode();
    }
}
