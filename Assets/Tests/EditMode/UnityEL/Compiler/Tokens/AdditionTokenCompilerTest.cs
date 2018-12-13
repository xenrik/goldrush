using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class AdditionTokenCompilerTest : BaseCompilerTest {
    [Test]
    public void TestSimpleAddition() {
        InitCompiler("1+2");

        UnityELExpression<int> expression = compiler.Compile<int>();
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(3, result);
    }
}