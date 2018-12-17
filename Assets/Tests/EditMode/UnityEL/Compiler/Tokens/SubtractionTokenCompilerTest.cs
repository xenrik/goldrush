using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class SubtractionTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerSubtraction() {
        UnityELExpression<int> expression = CreateExpression<int>("1-2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-1, result);
    }

    [Test]
    public void TestFloatSubtraction() {
        UnityELExpression<float> expression = CreateExpression<float>("1.2-2.3");
        float result = expression.Evaluate(evaluator);

        Assert.That(result, Is.EqualTo(-1.1f).Within(0.01f)); 
    }

    [Test]
    public void TestStringSubtraction() {
        UnityELExpression<int> expression = CreateExpression<int>("'1'-'2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-1, result);
    }

    [Test]
    public void TestStringNumberSubtraction() {
        UnityELExpression<int> expression = CreateExpression<int>("'1'-2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-1, result);
    }

    [Test]
    public void TestNumberStringSubtraction() {
        UnityELExpression<int> expression = CreateExpression<int>("1-'2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-1, result);
    }
}