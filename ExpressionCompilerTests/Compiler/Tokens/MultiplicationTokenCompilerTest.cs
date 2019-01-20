using NUnit.Framework;

public class MultiplicationTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerMultiplication() {
        UnityELExpression<float> expression = CreateExpression<float>("1*2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
    }

    [Test]
    public void TestFloatMultiplication() {
        UnityELExpression<float> expression = CreateExpression<float>("5.25 * 2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(10.5f, result);
    }

    [Test]
    public void TestStringMultiplication() {
        UnityELExpression<float> expression = CreateExpression<float>("'1'*'2'");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
    }

    [Test]
    public void TestStringNumberMultiplication() {
        UnityELExpression<float> expression = CreateExpression<float>("'1'*2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
    }

    [Test]
    public void TestNumberStringMultiplication() {
        UnityELExpression<float> expression = CreateExpression<float>("1*'2'");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
    }
}