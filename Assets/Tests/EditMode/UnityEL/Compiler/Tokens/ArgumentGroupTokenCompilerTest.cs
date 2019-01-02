using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Reflection;

public class ArgumentGroupTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;
    public ArgumentGroupEvaluator groupEvaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        evaluator.ArgumentGroupEvaluator = new TestArgumentGroupEvaluator();
        evaluator.DefaultFunctionResolver = new TestFunctionResolver();

        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestValueEvaluation() {
        UnityELExpression<object> expression = CreateExpression<object>("{1,2}");
        object result = expression.Evaluate(evaluator);

        Assert.AreEqual(new TestObject(1,2), result);
    }

    [Test]
    public void TestFunctionEvaluation() {
        UnityELExpression<object> expression = CreateExpression<object>("myFunction(1, {2,3}, 4)");
        object result = expression.Evaluate(evaluator);

        Assert.AreEqual(new TestObject2(12,16), result);
    }

    private class TestObject {
        public int a;
        public int b;

        public TestObject(int a, int b) {
            this.a = a;
            this.b = b;
        }

        public override int GetHashCode() {
            return (3 * a) + (7 * b);
        }

        public override bool Equals(object obj) {
            if (obj.GetType() != this.GetType()) {
                return false;
            }

            TestObject other = (TestObject)obj;
            return a == other.a && b == other.b;
        }
    }

    private class TestObject2 : TestObject {
        public TestObject2(int a, int b) : base(a, b) {
        }
    }

    private class TestArgumentGroupEvaluator : ArgumentGroupEvaluator {
        public object Evaluate(UnityELEvaluator context, ArgumentGroupToken group) {
            int arg1 = TypeCoercer.CoerceToType<int>(group, group.Children[0].Evaluate(context));
            int arg2 = TypeCoercer.CoerceToType<int>(group, group.Children[1].Evaluate(context));

            return new TestObject(arg1, arg2);
        }

        public object EvaluateForArgument(UnityELEvaluator context, string functionname, int argumentIndex, ArgumentGroupToken group) {
            if (functionname.Equals("myFunction")) {
                int arg1 = TypeCoercer.CoerceToType<int>(group, group.Children[0].Evaluate(context));
                int arg2 = TypeCoercer.CoerceToType<int>(group, group.Children[1].Evaluate(context));

                return new TestObject2(arg1, arg2);
            } else {
                return null;
            }
        }
    }

    private class TestFunctionResolver : FunctionResolver {
        public MethodInfo ResolveFunction(string name, Type[] argumentTypes) {
            if (name.Equals("myFunction")) {
                return GetType().GetMethod("Evaluate");
            } else {
                return null;
            }
        }

        public static TestObject2 Evaluate(int a, TestObject2 b, int c) {
            return new TestObject2((a + b.a) * c, (a + b.b) * c);
        }
    }
}