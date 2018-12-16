using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Reflection;

public class FunctionTokenCompilerTest : BaseCompilerTest {
    public UnityELEvaluator evaluator;
    public FunctionResolver functionResolver;

    [SetUp]
    public void Init() {
        evaluator = new UnityELEvaluator();

        functionResolver = new TestFunctionResolver();
        evaluator.DefaultFunctionResolver = functionResolver;
    }

    [Test]
    public void TestSimpleFunction() {
        UnityELExpression<string> expression = evaluator.Compile<string>("GetValue()");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("Value", result);
    }

    [Test]
    public void TestInstanceFunction() {
        evaluator.Properties["host"] = new TestObject();

        UnityELExpression<string> expression = evaluator.Compile<string>("host.GetValue()");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("Value", result);
    }

    private class TestFunctionResolver : FunctionResolver {
        public MethodInfo ResolveFunction(string name, Type[] argumentTypes) {
            if (name.Equals("GetValue")) {
                return GetType().GetMethod("GetValue");
            } else {
                return null;
            }
        }

        public static string GetValue() {
            return "Value";
        }
    }

    private class TestObject {
        public string GetValue() {
            return "Value";
        }
    }
}