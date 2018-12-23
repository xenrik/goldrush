using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ModulusTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestModulus() {
        UnityELExpression<int> expression = CreateExpression<int>("9%5");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(4, result);
    }
    
    [Test]
    public void TestStringModulus() {
        UnityELExpression<int> expression = CreateExpression<int>("'9'%'5'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(4, result);
    }
}