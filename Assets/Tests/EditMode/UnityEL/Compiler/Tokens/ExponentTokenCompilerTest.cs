using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ExponentTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerExponent() {
        UnityELExpression<float> expression = CreateExpression<float>("2**2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(4, result);
    }

    [Test]
    public void TestFloatExponent() {
        UnityELExpression<float> expression = CreateExpression<float>("2.5**2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(6.25f, result);
    }

    [Test]
    public void TestStringExponent() {
        UnityELExpression<float> expression = CreateExpression<float>("'3'**'3'");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(27f, result);
    }

    [Test]
    public void TestStringNumberExponent() {
        UnityELExpression<float> expression = CreateExpression<float>("'1'/2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(0.5f, result);
    }
}