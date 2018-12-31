using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class XorTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerXor() {
        UnityELExpression<int> expression = CreateExpression<int>("0b110 ^ 0b011");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(Convert.ToInt32("101", 2), result);
    }

    [Test]
    public void TestFloatXor() {
        UnityELExpression<int> expression = CreateExpression<int>("6.0 ^ 3");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(6 ^ 3, result);
    }

    [Test]
    public void TestStringXor() {
        UnityELExpression<int> expression = CreateExpression<int>("'6' ^ '3'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(6 ^ 3, result);
    }
}