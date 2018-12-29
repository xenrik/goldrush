using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class IsNotTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        evaluator.Properties["testObject"] = new TestObject();
        evaluator.Properties["nullProperty"] = null;

        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestStringIsNotString() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'abc' is not 'string'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestStringIsNotNotInt() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'abc' is not 'int'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestIntIsNotInt() {
        UnityELExpression<bool> expression = CreateExpression<bool>("123 is not 'int'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestFloatIsNotNotInt() {
        UnityELExpression<bool> expression = CreateExpression<bool>("123.5 is not 'int'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestZeroFloatIsNotInt() {
        UnityELExpression<bool> expression = CreateExpression<bool>("123.0 is not 'int'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestBoolIsNotBool() {
        UnityELExpression<bool> expression = CreateExpression<bool>("true is not 'bool'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestBoolIsNotBoolean() {
        UnityELExpression<bool> expression = CreateExpression<bool>("false is not 'boolean'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestTestObjectIsNotTestObject() {
        UnityELExpression<bool> expression = CreateExpression<bool>("testObject is not 'testObject'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestNullIsNotTestObject() {
        UnityELExpression<bool> expression = CreateExpression<bool>("null is not 'testObject'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestNullTokenIsNotNull() {
        UnityELExpression<bool> expression = CreateExpression<bool>("null is not null");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestNullPropertyIsNotNull() {
        UnityELExpression<bool> expression = CreateExpression<bool>("nullProperty is not null");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestStringIsNotNull() {
        UnityELExpression<bool> expression = CreateExpression<bool>("'abc' is not null");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    private class TestObject {
        public string MyString { get; private set; }

        public TestObject() {
            MyString = "myValue";
        }
    }
}