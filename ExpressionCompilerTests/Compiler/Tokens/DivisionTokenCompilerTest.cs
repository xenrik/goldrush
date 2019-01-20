using NUnit.Framework;

public class DivisionTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerDivision() {
        UnityELExpression<float> expression = CreateExpression<float>("1/2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(0.5f, result);
    }

    [Test]
    public void TestFloatDivision() {
        UnityELExpression<float> expression = CreateExpression<float>("10.5/2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(5.25f, result);
    }

    [Test]
    public void TestStringDivision() {
        UnityELExpression<float> expression = CreateExpression<float>("'1'/'2'");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(0.5f, result);
    }

    [Test]
    public void TestStringNumberDivision() {
        UnityELExpression<float> expression = CreateExpression<float>("'1'/2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(0.5f, result);
    }

    [Test]
    public void TestNumberStringDivision() {
        UnityELExpression<float> expression = CreateExpression<float>("1/'2'");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(0.5f, result);
    }
}