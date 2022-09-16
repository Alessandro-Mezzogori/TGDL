using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

using sf = TGDLLib.Syntax.SyntaxFactory;

namespace TGDLUnitTesting.TestingData.GenericSyntax;

internal class LambdaSyntaxDeclarationTestingData : ParserDataList<LambdaSyntaxDeclaration>
{
    public override List<DataUnit<string, LambdaSyntaxDeclaration>> DataList => new()
    {
        new()
        {
            Input = "bool Bool, player Player => 1\r\n", // TODO "Bool" need new expression parameteraccess,
            Output = sf.Lambda(
                sf.Body(
                    new[]{
                        sf.Expression(sf.Literal("1", sf.PredefinedType(TGDLType.Decimal)))
                    }
                ),
                new[]{
                    sf.Parameter(sf.PredefinedType(TGDLType.Bool), sf.Identifier("Bool")),
                    sf.Parameter(sf.SuppliedPredefinedType(TGDLType.Player), sf.Identifier("Player"))
                }
            )
        },
        new()
        {
            Input = "bool Bool, player Player => return 1", // TODO "Bool" need new expression parameteraccess,
            Output = sf.Lambda(
                sf.Body(
                    new[]{
                        sf.Return(sf.Literal("1", sf.PredefinedType(TGDLType.Decimal)))
                    }
                ),
                new[]{
                    sf.Parameter(sf.PredefinedType(TGDLType.Bool), sf.Identifier("Bool")),
                    sf.Parameter(sf.SuppliedPredefinedType(TGDLType.Player), sf.Identifier("Player"))
                }
            )
        },
        new()
        {
            Input = "player Player => ", // TODO "Bool" need new expression parameteraccess,
            Output = sf.Lambda(
                sf.Body(
                    new[]{
                        sf.Return(sf.Literal("1", sf.PredefinedType(TGDLType.Decimal)))
                    }
                ),
                new[]{
                    sf.Parameter(sf.SuppliedPredefinedType(TGDLType.Player), sf.Identifier("Player"))
                }
            ),
            Test = TestType.Fail
        },
        new()
        {
            Input = "player Player => 1 + 1 ", // TODO "Bool" need new expression parameteraccess,
            Output = sf.Lambda(
                sf.Body(
                    new[]{
                        sf.Return(
                            sf.BinaryOperation(
                                sf.Literal("1", sf.PredefinedType(TGDLType.Decimal)),
                                sf.Literal("1", sf.PredefinedType(TGDLType.Decimal)),
                                OperationKind.Addition
                            )
                        )
                    }
                ),
                new[]{
                    sf.Parameter(sf.SuppliedPredefinedType(TGDLType.Player), sf.Identifier("Player"))
                }
            ),
            Test = TestType.Fail
        },
        new()
        {
            Input = @"=> 1 + 1 ", // TODO "Bool" need new expression parameteraccess,
            Output = sf.Lambda(
                sf.Body(
                    new[]{
                        sf.Return(
                            sf.BinaryOperation(
                                sf.Literal("1", sf.PredefinedType(TGDLType.Decimal)),
                                sf.Literal("1", sf.PredefinedType(TGDLType.Decimal)),
                                OperationKind.Addition
                            )
                        )
                    }
                )
            ),
            Test = TestType.Fail
        },
    };
}

internal class LambdaSyntaxDeclarationComparer : IEqualityComparer<LambdaSyntaxDeclaration>
{
    private readonly ParameterSyntaxDeclarationComparer _parameterComparer = new();
    private readonly BodySyntaxDeclarationComparer _bodyComparer = new();
    private readonly TypeSyntaxComparer _typeComparer = new();

    public bool Equals(LambdaSyntaxDeclaration? x, LambdaSyntaxDeclaration? y)
    {
        if(x == null && y == null) return true;
        if(x == null || y == null) return false;

        // Parameters comparison
        return  x.Parameters.SequenceEqual(y.Parameters, _parameterComparer)
                && _bodyComparer.Equals(x.Body, y.Body)
                && _typeComparer.Equals(x.ReturnType, y.ReturnType);
    }

    public int GetHashCode([DisallowNull] LambdaSyntaxDeclaration obj)
    {
        return obj.GetHashCode();
    }
}
