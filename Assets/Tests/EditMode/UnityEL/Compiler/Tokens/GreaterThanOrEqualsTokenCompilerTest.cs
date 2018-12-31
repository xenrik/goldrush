using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class GreaterThanOrEqualsTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerGreaterThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 >= 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestInteger2GreaterThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("2 >= 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestFloatGreaterThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("10.5 >= 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestStringGreaterThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' >= '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestStringNumberGreaterThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' >= 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestNumberStringGreaterThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 >= '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }
}