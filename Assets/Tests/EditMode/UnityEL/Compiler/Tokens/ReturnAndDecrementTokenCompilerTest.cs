using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class ReturnAndDecrementTokenCompilerTest : BaseCompilerTest {
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
    public void TestNewPropertyReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("b--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(-1, evaluator.Properties["b"]);
    }

    [Test]
    public void TestIntegerPropertyReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("a--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(10, result);
        Assert.AreEqual(9, evaluator.Properties["a"]);
    }

    [Test]
    public void TestFloatPropertyReturnAndDecrement() {
        UnityELExpression<float> expression = CreateExpression<float>("a--");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(10.0f, result);
        Assert.AreEqual(9.0f, evaluator.Properties["a"]);
    }

    [Test]
    public void TestStringPropertyReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("a--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(10, result);
        Assert.AreEqual(9, evaluator.Properties["a"]);
    }

    [Test]
    public void TestExistingDictionaryElementReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['a']--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(10, result);
        Assert.AreEqual(9, dictionary["a"]);
    }

    [Test]
    public void TestNewDictionaryElementReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['b']--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(-1, dictionary["b"]);
    }

    [Test]
    public void TestExistingListElementReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("list[0]--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(10, result);
        Assert.AreEqual(9, list[0]);
    }

    [Test]
    public void TestNewListElementReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("list[1]--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(-1, list[1]);
    }

    [Test]
    public void TestArrayElementReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("array[0]--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(10, result);
        Assert.AreEqual(9, array[0]);
    }

    [Test]
    public void TestObjectReturnAndDecrement() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.Property--");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(10, result);
        Assert.AreEqual(9, testObject.Property);
    }

    private class TestObject {
        public int Property { get; set; }
    }

}