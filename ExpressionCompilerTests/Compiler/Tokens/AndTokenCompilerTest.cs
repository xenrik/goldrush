using NUnit.Framework;

public class AndTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestTrueAndTrue() {
        UnityELExpression<bool> expression = CreateExpression<bool>("true && true");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestTrueAndFalse() {
        UnityELExpression<bool> expression = CreateExpression<bool>("true && false");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestFalseAndTrue() {
        UnityELExpression<bool> expression = CreateExpression<bool>("false && true");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestFalseAndFalse() {
        UnityELExpression<bool> expression = CreateExpression<bool>("false && false");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }
}