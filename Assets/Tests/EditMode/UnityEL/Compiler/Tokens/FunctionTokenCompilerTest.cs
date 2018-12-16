using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Reflection;
using System.Text;

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
    public void TestFunctionWithArgument() {
        UnityELExpression<string> expression = evaluator.Compile<string>("SayHello('Lee')");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("Hello Lee", result);
    }

    [Test]
    public void TestFunctionWithCastArgument() {
        UnityELExpression<string> expression = evaluator.Compile<string>("SayHello(123)");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("Hello 123", result);
    }

    [Test]
    public void TestFunctionWithVariableArguments() {
        UnityELExpression<string> expression = evaluator.Compile<string>("TestVarArgs('A','B','C')");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("[A,B,C]", result);
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
            } else if (name.Equals("SayHello")) {
                return GetType().GetMethod("SayHello");
            } else if (name.Equals("TestVarArgs")) {
                return GetType().GetMethod("TestVarArgs");
            } else {
                return null;
            }
        }

        public static string GetValue() {
            return "Value";
        }

        public static string SayHello(string name) {
            return "Hello " + name;
        }

        public static string TestVarArgs(params string[] name) {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("[");
            for (int i = 0; i < name.Length; ++i) {
                buffer.Append(name[i]);
                if (i + 1 < name.Length) {
                    buffer.Append(",");
                }
            }
            buffer.Append("]");

            return buffer.ToString();
        }
    }

    private class TestObject {
        public string GetValue() {
            return "Value";
        }
    }
}