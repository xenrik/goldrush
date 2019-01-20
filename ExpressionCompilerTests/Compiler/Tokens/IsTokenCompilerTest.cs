using NUnit.Framework;

public class IsTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        evaluator.Properties["testObject"] = new TestObject();
        evaluator.Properties["nullProperty"] = null;

        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestStringIsString() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'abc' is 'string'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestStringIsNotInt() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'abc' is 'int'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestIntIsInt() {
        UnityELExpression<bool> expression = CreateExpression<bool>("123 is 'int'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestFloatIsNotInt() {
        UnityELExpression<bool> expression = CreateExpression<bool>("123.5 is 'int'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestZeroFloatIsInt() {
        UnityELExpression<bool> expression = CreateExpression<bool>("123.0 is 'int'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestBoolIsBool() {
        UnityELExpression<bool> expression = CreateExpression<bool>("true is 'bool'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestBoolIsBoolean() {
        UnityELExpression<bool> expression = CreateExpression<bool>("false is 'boolean'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestTestObjectIsTestObject() {
        UnityELExpression<bool> expression = CreateExpression<bool>("testObject is 'testObject'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestNullIsNotTestObject() {
        UnityELExpression<bool> expression = CreateExpression<bool>("null is 'testObject'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestNullTokenIsNull() {
        UnityELExpression<bool> expression = CreateExpression<bool>("null is null");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestNullPropertyIsNull() {
        UnityELExpression<bool> expression = CreateExpression<bool>("nullProperty is null");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestStringIsNotNull() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'abc' is null");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    private class TestObject {
        public string MyString { get; private set; }

        public TestObject() {
            MyString = "myValue";
        }
    }
}