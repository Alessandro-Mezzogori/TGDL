namespace TGDLLib.Syntax;


public class BinaryOperationExpressionSyntax : ExpressionSyntax
{
    public ExpressionSyntax LeftOperand { get; }
    public ExpressionSyntax RightOperand { get; }
    
    public BinaryOperationExpressionSyntax(ExpressionSyntax leftOperand, OperationKind operation, ExpressionSyntax rightOperand)
        : base(operation)
    {
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
    }
}

public class PowerOperationSyntax : BinaryOperationExpressionSyntax
{
    public PowerOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.Power, right) { }
}
public class AddOperationSyntax : BinaryOperationExpressionSyntax
{
    public AddOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.Addition, right) { }
}
public class SubOperationSyntax : BinaryOperationExpressionSyntax
{
    public SubOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.Subtraction, right) { }
}
public class DivideOperationSyntax : BinaryOperationExpressionSyntax
{
    public DivideOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.Division, right) { }
}
public class MultiplyOperationSyntax : BinaryOperationExpressionSyntax
{
    public MultiplyOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.Moltiplication, right) { }
}

public class ModuloOperationSyntax : BinaryOperationExpressionSyntax
{
    public ModuloOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.Modulo, right) { }
}

public class GreaterThanOperationSyntax : BinaryOperationExpressionSyntax
{
    public GreaterThanOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.GreaterThan, right) { }
}

public class GreaterOrEqualOperationSyntax: BinaryOperationExpressionSyntax
{
    public GreaterOrEqualOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.GreaterOrEqual, right) { }
}

public class LessThanOperationSyntax: BinaryOperationExpressionSyntax
{
    public LessThanOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.LessThan, right) { }
}
public class LessOrEqualOperationSyntax : BinaryOperationExpressionSyntax
{
    public LessOrEqualOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.LessOrEqual, right) { }
}
public class EqualOperationSyntax : BinaryOperationExpressionSyntax
{
    public EqualOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.Equal, right) { }
}
public class NotEqualOperationSyntax : BinaryOperationExpressionSyntax
{
    public NotEqualOperationSyntax(ExpressionSyntax left, ExpressionSyntax right) : base(left, OperationKind.NotEqual, right) { }
}



