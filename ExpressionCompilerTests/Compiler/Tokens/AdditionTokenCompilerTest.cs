using NUnit.Framework;

public class AdditionTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerAddition() {
        UnityELExpression<int> expression = CreateExpression<int>("1+2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(3, result);
    }

    [Test]
    public void TestFloatAddition() {
        UnityELExpression<float> expression = CreateExpression<float>("1.2+2.3");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(3.5f, result);
    }

    [Test]
    public void TestStringAddition() {
        UnityELExpression<string> expression = CreateExpression<string>("'1'+'2'");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("12", result);
    }

    [Test]
    public void TestStringNumberAddition() {
        UnityELExpression<string> expression = CreateExpression<string>("'1'+2");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("12", result);
    }

    [Test]
    public void TestNumberStringAddition() {
        UnityELExpression<int> expression = CreateExpression<int>("1+'2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(3, result);
    }
}