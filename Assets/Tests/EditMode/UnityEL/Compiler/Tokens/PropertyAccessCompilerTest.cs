using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PropertyAccessCompilerTest : BaseCompilerTest {
    public UnityELEvaluator evaluator;

    [SetUp]
    public void InitCompiler(string expression) {
        evaluator = new UnityELEvaluator();
    }

    [Test]
    public void TestRootObjectAccess() {
        evaluator.Properties["myString"] = "myValue";

        UnityELExpression<string> expression = evaluator.Compile<string>("myHost.String");

        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("myValue", result);
    }
}