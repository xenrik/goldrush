using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class SubtractAndAssignTokenCompilerTest : BaseCompilerTest {
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
    public void TestNewPropertyDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("b -= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-2, result);
        Assert.AreEqual(-2, evaluator.Properties["b"]);
    }

    [Test]
    public void TestIntegerPropertyDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a -= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(8, result);
        Assert.AreEqual(8, evaluator.Properties["a"]);
    }

    [Test]
    public void TestFloatPropertyDecrementAndAssign() {
        UnityELExpression<float> expression = CreateExpression<float>("a -= 5.5");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(4.5f, result);
        Assert.AreEqual(4.5f, evaluator.Properties["a"]);
    }

    [Test]
    public void TestStringPropertyDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a -= '2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(8, result);
        Assert.AreEqual(8, evaluator.Properties["a"]);
    }

    [Test]
    public void TestExistingDictionaryElementDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['a'] -= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(8, result);
        Assert.AreEqual(8, dictionary["a"]);
    }

    [Test]
    public void TestNewDictionaryElementDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['b'] -= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-2, result);
        Assert.AreEqual(-2, dictionary["b"]);
    }

    [Test]
    public void TestExistingListElementDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[0] -= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(8, result);
        Assert.AreEqual(8, list[0]);
    }

    [Test]
    public void TestNewListElementDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[1] -= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(-2, result);
        Assert.AreEqual(-2, list[1]);
    }

    [Test]
    public void TestArrayElementDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("array[0] -= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(8, result);
        Assert.AreEqual(8, array[0]);
    }

    [Test]
    public void TestObjectDecrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.Property -= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(8, result);
        Assert.AreEqual(8, testObject.Property);
    }

    private class TestObject {
        public int Property { get; set; }
    }

}