using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ConditionalOperatorTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestTrueResult() {
        UnityELExpression<int> expression = CreateExpression<int>("true ? 1 : 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestFalseResult() {
        UnityELExpression<int> expression = CreateExpression<int>("false ? 1 : 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
    }

    [Test]
    public void TestComplexTestTrueResult() {
        UnityELExpression<int> expression = CreateExpression<int>("(true || false) ? 1 : 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestComplexTrueResult() {
        UnityELExpression<int> expression = CreateExpression<int>("true ? (1+1) : 3");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
    }

    [Test]
    public void TestComplexFalseResult() {
        UnityELExpression<int> expression = CreateExpression<int>("false ? 1 : (2*3)");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(6, result);
    }
}