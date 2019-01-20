using NUnit.Framework;

public class EqualsTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 == 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestInteger2Equals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("2 == 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestFloatEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("10.5 == 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestFloat2Equals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("10.5 == 10.5");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestStringEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' == '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestString2Equals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'abc' == 'abc'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestStringNumberEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'1' == 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestString2NumberEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'2' == 2");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestNumberStringEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("1 == '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestNumber2StringEquals() {
        UnityELExpression<bool> expression = CreateExpression<bool>("2 == '2'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }
}