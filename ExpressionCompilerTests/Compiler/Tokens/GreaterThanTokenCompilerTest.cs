using NUnit.Framework;

public class GreaterThanTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerGreaterThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 > 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestInteger2GreaterThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("2 > 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestFloatGreaterThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("10.5 > 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestStringGreaterThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' > '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestStringNumberGreaterThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' > 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestNumberStringGreaterThan() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 > '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }
}