using NUnit.Framework;
using System;

public class ComplementTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerComplement() {
        UnityELExpression<int> expression = CreateExpression<int>("~0b1001");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(~Convert.ToInt32("1001", 2), result);
    }

    [Test]
    public void TestFloatComplement() {
        UnityELExpression<int> expression = CreateExpression<int>("~9.0");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(~9, result);
    }

    [Test]
    public void TestStringComplement() {
        UnityELExpression<int> expression = CreateExpression<int>("~'9'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(~9, result);
    }
}