using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Statements;

namespace TGDLUnitTesting.TestingData;

using sf = SyntaxFactory;

internal class StatementSyntaxTestingData : ParserDataList<StatementSyntax>
{
    public override List<DataUnit<string, StatementSyntax>> DataList => new()
    {
        new()
        {
            Input = "return 1",
            Output = sf.Return(sf.Literal("1", TGDLType.Decimal))
        },
        new()
        {
            Input = "return     1 + 1",
            Output = sf.Return(
                sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal),
                    sf.Literal("1", TGDLType.Decimal),
                    OperationKind.Addition 
                )
            )
        },
        new()
        {
            Input = "x = 1",
            Output = sf.Assignment(sf.IdentifierName("x"), sf.Literal("1", TGDLType.Decimal))
        },
        new()
        {
            Input = "x = 1 + 2",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal),
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.Addition
                )
            )
        }
    };
}

internal class StatementSyntaxComparer : IEqualityComparer<StatementSyntax>
{

    public bool Equals(StatementSyntax? x, StatementSyntax? y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;

        if (ComparerHelpers.CompareIfType(x, y, out var resultReturn, new ReturnStatementSyntaxComparer())) return resultReturn;
        if (ComparerHelpers.CompareIfType(x, y, out var resultAssignment, new AssignmentStatementSyntaxComparer())) return resultAssignment;
        if (ComparerHelpers.CompareIfType(x, y, out var resultExpression, new ExpressionStatementComparer())) return resultExpression;

        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] StatementSyntax obj)
    {
        return obj.GetHashCode();
    }
}

internal static class ComparerHelpers
{
    /// <summary>
    /// Handles the equality comparison between two object if they are of the given type T
    /// </summary>
    /// <typeparam name="T">type to check against</typeparam>
    /// <param name="x">first object</param>
    /// <param name="y">second object</param>
    /// <param name="result">result of the comparison</param>
    /// <param name="comparer">used comparer, if null default will be used</param>
    /// <returns>true if the comparison was handled, otherwise false</returns>
    public static bool CompareIfType<T>(object x, object y, out bool result, IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        if (x is T xT && y is T yT)
        {
            result = comparer.Equals(xT, yT);
            return true;
        }
        
        result = false;
        return false;
    }
}
