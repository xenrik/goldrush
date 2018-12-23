using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Reflection;
using System;

public class CombinedCompilerTest : BaseCompilerTest {
    public UnityELEvaluator evaluator;
    public FunctionResolver functionResolver;

    [SetUp]
    public void Init() {
        evaluator = new UnityELEvaluator();

        functionResolver = new TestFunctionResolver();
        evaluator.DefaultFunctionResolver = functionResolver;
    }

    public string GetMultiPartExpressionBase() {
        return "(a + 3.5) * b.Value / GetValue() - 2";
    }

    [Test]
    public void testMultipartExpression() {
        evaluator.Properties["a"] = 4;
        evaluator.Properties["b"] = new TestObject();

        UnityELExpression<int> expression = 
            evaluator.Compile<int>(GetMultiPartExpressionBase());
        int result = expression.Evaluate(evaluator);

        // result = (4 + 3.5) * 2 / 3 - 2
        // result = 7.5 * 2 / 3 - 2
        // result = 15 / 3 - 2
        // result = 5 - 2
        // result = 3;
        Assert.AreEqual(3, result);
    }

    [Test]
    public void testPrecedence() {
        UnityELExpression<int> expression =
            evaluator.Compile<int>("1 + 2 * 3");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(7, result);
    }

    private class TestFunctionResolver : FunctionResolver {
        public MethodInfo ResolveFunction(string name, Type[] argumentTypes) {
            if (name.Equals("GetValue")) {
                return GetType().GetMethod("GetValue");
            } else {
                return null;
            }
        }

        public static int GetValue() {
            return 3;
        }
    }

    private class TestObject {
        public float Value { get { return 2.0f; } }
    }
}