using NUnit.Framework;

public class OrTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestTrueOrTrue() {
        UnityELExpression<bool> expression = CreateExpression<bool>("true || true");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestTrueOrFalse() {
        UnityELExpression<bool> expression = CreateExpression<bool>("true || false");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestFalseOrTrue() {
        UnityELExpression<bool> expression = CreateExpression<bool>("false || true");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestFalseOrFalse() {
        UnityELExpression<bool> expression = CreateExpression<bool>("false || false");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }
}