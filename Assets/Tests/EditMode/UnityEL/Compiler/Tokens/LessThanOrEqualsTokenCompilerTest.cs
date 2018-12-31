using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class LessThanOrEqualsTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerLessThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 <= 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestInteger2LessThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("2 <= 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestFloatLessThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("10.5 <= 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestStringLessThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' <= '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestStringNumberLessThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' <= 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestNumberStringLessThanOrEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 <= '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }
}