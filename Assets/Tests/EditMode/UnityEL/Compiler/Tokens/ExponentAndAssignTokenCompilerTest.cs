using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using System;

public class ExponentAndAssignTokenCompilerTest : BaseCompilerTest {
    private ExpressionCompiler compiler;
    private UnityELEvaluator evaluator;

    private Dictionary<string, int> dictionary;
    private List<int> list;
    private int[] array;

    private TestObject testObject;

    private UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        evaluator.Properties["a"] = 10;

        dictionary = new Dictionary<string, int>();
        dictionary["a"] = 10;
        evaluator.Properties["dictionary"] = dictionary;

        list = new List<int>();
        list.Add(10);
        evaluator.Properties["list"] = list;

        array = new int[2];
        array[0] = 10;
        evaluator.Properties["array"] = array;

        testObject = new TestObject();
        testObject.Property = 10;

        evaluator.Properties["testObject"] = testObject;

        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestNewPropertyExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("b **= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, evaluator.Properties["b"]);
    }

    [Test]
    public void TestIntegerPropertyExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a **= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(100, result);
        Assert.AreEqual(100, evaluator.Properties["a"]);
    }

    [Test]
    public void TestFloatPropertyExponentAndAssign() {
        UnityELExpression<float> expression = CreateExpression<float>("a **= 2.5");
        float result = expression.Evaluate(evaluator);

        Assert.That(result, Is.EqualTo(Math.Pow(10, 2.5)).Within(0.01f));
        Assert.That(evaluator.Properties["a"], Is.EqualTo(Math.Pow(10, 2.5)).Within(0.01f));
    }

    [Test]
    public void TestStringPropertyExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a **= '2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(100, result);
        Assert.AreEqual(100, evaluator.Properties["a"]);
    }

    [Test]
    public void TestExistingDictionaryElementExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['a'] **= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(100, result);
        Assert.AreEqual(100, dictionary["a"]);
    }

    [Test]
    public void TestNewDictionaryElementExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['b'] **= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, dictionary["b"]);
    }

    [Test]
    public void TestExistingListElementExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[0] **= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(100, result);
        Assert.AreEqual(100, list[0]);
    }

    [Test]
    public void TestNewListElementExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[1] **= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, list[1]);
    }

    [Test]
    public void TestArrayElementExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("array[0] **= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(100, result);
        Assert.AreEqual(100, array[0]);
    }

    [Test]
    public void TestObjectExponentAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.Property **= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(100, result);
        Assert.AreEqual(100, testObject.Property);
    }

    private class TestObject {
        public int Property { get; set; }
    }

}