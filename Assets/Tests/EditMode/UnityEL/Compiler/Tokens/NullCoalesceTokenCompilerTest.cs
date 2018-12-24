using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class NullCoalesceTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        evaluator.Properties["nullLHS"] = null;
        evaluator.Properties["nonNullLHS"] = true;
        evaluator.Properties["nullRHS"] = null;
        evaluator.Properties["nonNullRHS"] = false;

        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestLHSNull() {
        UnityELExpression<object> expression = CreateExpression<object>("nullLHS ?? nonNullRHS");
        object result = expression.Evaluate(evaluator);

        Assert.AreEqual(false, result);
    }

    [Test]
    public void TestRHSNull() {
        UnityELExpression<object> expression = CreateExpression<object>("nonNullLHS ?? nullRHS");
        object result = expression.Evaluate(evaluator);

        Assert.AreEqual(true, result);
    }

    [Test]
    public void TestNeitherNull() {
        UnityELExpression<object> expression = CreateExpression<object>("nonNullLHS ?? nonNullRHS");
        object result = expression.Evaluate(evaluator);

        Assert.AreEqual(true, result);
    }

    [Test]
    public void TestBothNull() {
        UnityELExpression<object> expression = CreateExpression<object>("nullLHS ?? nullRHS");
        object result = expression.Evaluate(evaluator);

        Assert.IsNull(result);
    }
}