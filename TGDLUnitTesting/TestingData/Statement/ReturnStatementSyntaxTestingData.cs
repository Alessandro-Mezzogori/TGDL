﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGDLLib.Syntax;

namespace TGDLUnitTesting.TestingData
{
    internal class ReturnStatementSyntaxTestingData : ParserDataList<ReturnStatementSyntax>
    {
        public override List<DataUnit<string, ReturnStatementSyntax>> DataList => new()
        {
            new()
            {
                Input = "return this.test",
                Output = new(
                    new AttributeAccessExpressionSyntax(new("this"), new("test"))
                )
            },
            new()
            {
                Input = "return test",
                Output = new(
                    new AttributeAccessExpressionSyntax(new("this"), new("test"))
                )
            },
            new()
            {
                Input = "return 1 + 1",
                Output = new(
                    new BinaryOperationExpressionSyntax(
                        new LiteralExpressionSyntax("1", TGDLType.Decimal),
                        OperatorKind.Addition,
                        new LiteralExpressionSyntax("1", TGDLType.Decimal)
                    )
                )
            },
            new()
            {
                Input = "return 1",
                Output = new(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal)
                )
            },
            new()
            {
                Input = "1",
                Output = new(
                    new LiteralExpressionSyntax("1", TGDLType.Decimal)
                ),
                Test = TestType.Fail
            },

        };
    }

    internal class ReturnStatementSyntaxComparer : IEqualityComparer<ReturnStatementSyntax>
    {
        private ExpressionSyntaxComparer _comparer = new();
        public bool Equals(ReturnStatementSyntax? x, ReturnStatementSyntax? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            return _comparer.Equals(x.Expression, y.Expression);
        }

        public int GetHashCode([DisallowNull] ReturnStatementSyntax obj)
        {
            return obj.GetHashCode();
        }
    }
}
