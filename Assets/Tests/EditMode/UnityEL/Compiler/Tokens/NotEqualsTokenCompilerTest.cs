using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class NotEqualsTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerNotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 != 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestInteger2NotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("2 != 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestFloatNotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("10.5 != 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestFloat2NotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("10.5 != 10.5");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestStringNotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' != '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestString2NotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'abc' != 'abc'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestStringNumberNotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' != 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestString2NumberNotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'2' != 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestNumberStringNotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 != '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestNumber2StringNotEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("2 != '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }
}