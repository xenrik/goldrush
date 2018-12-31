using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class LessThanTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerLessThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 < 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestInteger2LessThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("2 < 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestFloatLessThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("10.5 < 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestStringLessThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' < '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestStringNumberLessThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' < 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestNumberStringLessThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 < '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }
}