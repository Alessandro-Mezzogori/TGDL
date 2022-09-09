﻿using System.Diagnostics.CodeAnalysis;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData.GenericSyntax;

internal class LambdaSyntaxDeclarationTestingData : ParserDataList<LambdaSyntaxDeclaration>
{
    public override List<DataUnit<string, LambdaSyntaxDeclaration>> DataList => new()
    {
        
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
        return !x.Parameters.Except(y.Parameters, _parameterComparer).Any()
                && _bodyComparer.Equals(x.Body, y.Body)
                && _typeComparer.Equals(x.ReturnType, y.ReturnType);
    }

    public int GetHashCode([DisallowNull] LambdaSyntaxDeclaration obj)
    {
        return obj.GetHashCode();
    }
}
