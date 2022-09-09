using System.Linq.Expressions;

namespace TGDLLib.Syntax;

public enum ComparisonOperator
{
    Equal,
    NotEqual,
    GreaterThan,
    LessThan,
    GreaterOrEqual,
    LessOrEqual,
}

public class ComparisonExpressionSyntax : ExpressionSyntax
{
    public ExpressionSyntax LeftOperand{ get; }
    public ExpressionSyntax RightOperand{ get; }
    public ComparisonOperator Operator { get; }

    public ComparisonExpressionSyntax(ExpressionSyntax leftOperand, ExpressionSyntax rightOperand, ComparisonOperator @operator)
    {
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
        Operator = @operator;
    }
}
