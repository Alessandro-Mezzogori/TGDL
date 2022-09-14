using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;
using TGDLLib.Syntax.Statements;

namespace TGDLUnitTesting.TestingData;

using sf = TGDLLib.Syntax.SyntaxFactory;

internal class AssignmentStatementSyntaxTestingData : ParserDataList<AssignmentStatementSyntax>
{
    public override List<DataUnit<string, AssignmentStatementSyntax>> DataList => new()
    {
        new()
        {
            Input = "x = 1",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.Literal("1", TGDLType.Decimal)
            )
        },
        new()
        {
            Input = "x=1",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.Literal("1", TGDLType.Decimal)
            )
        },
        new()
        {
            Input = "x = 1 + 1",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.BinaryOperation(
                    sf.Literal("1", TGDLType.Decimal),
                    sf.Literal("1", TGDLType.Decimal),
                    OperationKind.Addition
                ) 
            )   
        },
        new()
        {
            Input = "x= testState.attribute",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.BinaryOperation(
                    sf.IdentifierName("testState"),
                    sf.IdentifierName("attribute"),
                    OperationKind.Dot
                )
            )
        },
        new()
        {
            Input = "x = true",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.Literal("true", TGDLType.Bool)
            )
        },
        new()
        {
            Input = "x = y + 2",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.BinaryOperation(
                    sf.IdentifierName("y"), 
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.Addition
                )
            )
        },
        new()
        {
            Input = "x = y ^ 2",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.BinaryOperation(
                    sf.IdentifierName("y"), 
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.Power
                )
            )
        },
        new()
        {
            Input = "x = y ^ 2 + 1",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.BinaryOperation(
                    sf.BinaryOperation(
                        sf.IdentifierName("y"), 
                        sf.Literal("2", TGDLType.Decimal),
                        OperationKind.Power
                    ),
                    sf.Literal("1", TGDLType.Decimal),
                    OperationKind.Addition
                )
            )
        },
        new()
        {
            Input = "x=",
            Test = TestType.Fail
        },
        new()
        {
            Input = @"x = 
                        10.0",
            Output = sf.Assignment(
                sf.IdentifierName("x"),
                sf.Literal("10.0", TGDLLib.Syntax.TGDLType.Decimal)
            )
        },
        new()
        {
            Input = "x.y = y - 2",
            Output = sf.Assignment(
                sf.BinaryOperation(
                    sf.IdentifierName("x"),
                    sf.IdentifierName("y"),
                    OperationKind.Dot 
                ),
                sf.BinaryOperation(
                    sf.IdentifierName("y"),
                    sf.Literal("2", TGDLType.Decimal),
                    OperationKind.Subtraction
                )
            )
        },

    };
}

internal class AssignmentStatementSyntaxComparer : IEqualityComparer<AssignmentStatementSyntax>
{
    private readonly ExpressionSyntaxComparer _expressionComparer = new();
    public bool Equals(AssignmentStatementSyntax? x, AssignmentStatementSyntax? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        return _expressionComparer.Equals(x.Identifier, y.Identifier)
            && _expressionComparer.Equals(x.Expression, y.Expression);
    }

    public int GetHashCode([DisallowNull] AssignmentStatementSyntax obj)
    {
        return obj.GetHashCode();
    }
}
