using NUnit.Framework;

public class IdentifierTokenCompilerTest : BaseCompilerTest {
    public UnityELEvaluator evaluator;

    [SetUp]
    public void InitCompiler() {
        evaluator = new UnityELEvaluator();
    }

    [Test]
    public void TestKnownProperty() {
        evaluator.Properties["Property"] = "Value";

        UnityELExpression<string> expression = evaluator.Compile<string>("Property");

        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("Value", result);
    }

    [Test]
    public void TestUnknownProperty() {
        evaluator.Properties["Property"] = "Value";

        UnityELExpression<string> expression = evaluator.Compile<string>("SomeOtherProperty");

        Assert.Throws<NoSuchPropertyException>(delegate {
            expression.Evaluate(evaluator);
        });
    }
}