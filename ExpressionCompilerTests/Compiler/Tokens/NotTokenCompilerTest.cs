using NUnit.Framework;

public class NotTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestNotTrue() {
        UnityELExpression<bool> expression = CreateExpression<bool>("!true");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestNotFalse() {
        UnityELExpression<bool> expression = CreateExpression<bool>("!false");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestStringNot() {
        UnityELExpression<bool> expression = CreateExpression<bool>("!'true'");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }
}