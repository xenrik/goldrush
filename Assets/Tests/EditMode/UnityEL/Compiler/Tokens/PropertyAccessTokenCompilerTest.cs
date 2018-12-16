using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PropertyAccessTokenCompilerTest : BaseCompilerTest {
    public UnityELEvaluator evaluator;

    [SetUp]
    public void InitCompiler() {
        evaluator = new UnityELEvaluator();
    }

    [Test]
    public void TestPropertyAccess() {
        evaluator.Properties["myHost"] = new TestObject();

        UnityELExpression<string> expression = evaluator.Compile<string>("myHost.MyString");

        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("myValue", result);
    }

    [Test]
    public void TestUnknownPropertyAccess() {
        evaluator.Properties["myHost"] = new TestObject();

        UnityELExpression<string> expression = evaluator.Compile<string>("myHost.UnknownString");

        Assert.Throws<NoSuchPropertyException>(delegate {
            expression.Evaluate(evaluator);
        });
    }

    private class TestObject {
        public string MyString { get; private set; }

        public TestObject() {
            MyString = "myValue";
        }
    }
}