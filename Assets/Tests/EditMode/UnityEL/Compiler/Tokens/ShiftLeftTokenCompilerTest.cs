using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class ShiftLeftTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerShiftLeft() {
        UnityELExpression<int> expression = CreateExpression<int>("0b110 << 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(Convert.ToInt32("11000", 2), result);
    }

    [Test]
    public void TestFloatShiftLeft() {
        UnityELExpression<int> expression = CreateExpression<int>("6.0 << 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(6 << 2, result);
    }

    [Test]
    public void TestStringShiftLeft() {
        UnityELExpression<int> expression = CreateExpression<int>("'6' << 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(6 << 2, result);
    }
}