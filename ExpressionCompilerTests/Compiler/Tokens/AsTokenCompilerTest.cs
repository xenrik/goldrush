using NUnit.Framework;

public class AsTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerAsInteger() {
        UnityELExpression<object> expression = CreateExpression<object>("1 as 'int'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<int>(result);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestStringAsInteger() {
        UnityELExpression<object> expression = CreateExpression<object>("'1' as 'int'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<int>(result);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestBoolAsInteger() {
        UnityELExpression<object> expression = CreateExpression<object>("true as 'int'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<int>(result);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestNullAsInteger() {
        UnityELExpression<object> expression = CreateExpression<object>("null as 'int'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<int>(result);
        Assert.AreEqual(0, result);
    }

    [Test]
    public void TestIntegerAsFloat() {
        UnityELExpression<object> expression = CreateExpression<object>("1 as 'float'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<float>(result);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestStringAsFloat() {
        UnityELExpression<object> expression = CreateExpression<object>("'1' as 'float'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<float>(result);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestBoolAsFloat() {
        UnityELExpression<object> expression = CreateExpression<object>("true as 'float'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<float>(result);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestNullAsFloat() {
        UnityELExpression<object> expression = CreateExpression<object>("null as 'float'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<float>(result);
        Assert.AreEqual(0, result);
    }

    [Test]
    public void TestIntegerAsString() {
        UnityELExpression<object> expression = CreateExpression<object>("1 as 'string'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<string>(result);
        Assert.AreEqual("1", result);
    }

    [Test]
    public void TestStringAsString() {
        UnityELExpression<object> expression = CreateExpression<object>("'1' as 'string'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<string>(result);
        Assert.AreEqual("1", result);
    }

    [Test]
    public void TestBoolAsString() {
        UnityELExpression<object> expression = CreateExpression<object>("true as 'string'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<string>(result);
        Assert.AreEqual("true", result);
    }

    [Test]
    public void TestNullAsString() {
        UnityELExpression<object> expression = CreateExpression<object>("null as 'string'");
        object result = expression.Evaluate(evaluator);

        Assert.IsNull(result);
    }

    [Test]
    public void TestIntegerAsBool() {
        UnityELExpression<object> expression = CreateExpression<object>("1 as 'bool'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<bool>(result);
        Assert.AreEqual(true, result);
    }

    [Test]
    public void TestStringAsBool() {
        UnityELExpression<object> expression = CreateExpression<object>("'true' as 'bool'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<bool>(result);
        Assert.AreEqual(true, result);
    }

    [Test]
    public void TestNullAsBool() {
        UnityELExpression<object> expression = CreateExpression<object>("null as 'bool'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<bool>(result);
        Assert.AreEqual(false, result);
    }

    [Test]
    public void TestBoolAsBool() {
        UnityELExpression<object> expression = CreateExpression<object>("true as 'bool'");
        object result = expression.Evaluate(evaluator);

        Assert.IsInstanceOf<bool>(result);
        Assert.AreEqual(true, result);
    }
}