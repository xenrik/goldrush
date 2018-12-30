using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

public class NotExistsTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;
    public FunctionResolver functionResolver;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        evaluator.Properties["property"] = "anc";
        evaluator.Properties["dictionary"] = new Dictionary<string, string> {
            { "key", "value" }
        };
        evaluator.Properties["list"] = new List<string> { "abc" };
        evaluator.Properties["array"] = new string[] { "abc" };
        evaluator.Properties["object"] = new TestObject();

        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestStringConstantExists() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists ''");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestPropertyExists() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists property");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestUnknownPropertyDoesntExist() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists unknown");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestDictionaryPropertyExists() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists dictionary['key']");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestUnknownDictionaryPropertyDoesntExist() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists dictionary['unknown']");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestListElementExists() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists list[0]");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestOutOfBoundsListElementDoesntExist() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists list[1]");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestArrayElementExists() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists array[0]");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestOutOfBoundsArrayElementDoesntExist() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists array[1]");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestObjectPropertyExists() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists object.Property");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestUnknownObjectPropertyDoesntExist() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists object.Unknown");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestObjectFunctionExists() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists object.Function()");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestUnknownObjectFunctionDoesntExist() {
        UnityELExpression<bool> expression = CreateExpression<bool>("not exists object.Unknown()");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    private class TestObject{
        public string Property { get; set; }
        public TestObject() {
        }

        public string Function() {
            return "";
        }
    }
}