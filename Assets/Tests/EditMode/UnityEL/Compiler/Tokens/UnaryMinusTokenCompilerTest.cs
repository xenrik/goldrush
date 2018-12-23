using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class UnaryMinusTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerUnaryMinus() {
        UnityELExpression<int> expression = CreateExpression<int>("-2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-2, result);
    }

    [Test]
    public void TestFloatUnaryMinus() {
        UnityELExpression<float> expression = CreateExpression<float>("-2.3");
        float result = expression.Evaluate(evaluator);

        Assert.That(result, Is.EqualTo(-2.3f).Within(0.01f)); 
    }

    [Test]
    public void TestStringUnaryMinus() {
        UnityELExpression<int> expression = CreateExpression<int>("-'2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-2, result);
    }
}