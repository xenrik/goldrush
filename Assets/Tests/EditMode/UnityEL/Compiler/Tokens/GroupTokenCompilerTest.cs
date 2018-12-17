using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class GroupTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerAddition() {
        UnityELExpression<int> expression = CreateExpression<int>("(1+2)");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(3, result);
    }

    [Test]
    public void TestDoubleGroupAddition() {
        UnityELExpression<int> expression = CreateExpression<int>("((1+2) + 3)");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(6, result);
    }
}