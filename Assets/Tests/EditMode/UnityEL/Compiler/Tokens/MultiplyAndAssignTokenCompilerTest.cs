using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class MultiplyAndAssignTokenCompilerTest : BaseCompilerTest {
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
    public void TestNewPropertyMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("b *= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, evaluator.Properties["b"]);
    }

    [Test]
    public void TestIntegerPropertyMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a *= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(20, result);
        Assert.AreEqual(20, evaluator.Properties["a"]);
    }

    [Test]
    public void TestFloatPropertyMultiplyAndAssign() {
        UnityELExpression<float> expression = CreateExpression<float>("a *= 5.5");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(55f, result);
        Assert.AreEqual(55f, evaluator.Properties["a"]);
    }

    [Test]
    public void TestStringPropertyMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a *= '2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(20, result);
        Assert.AreEqual(20, evaluator.Properties["a"]);
    }

    [Test]
    public void TestExistingDictionaryElementMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['a'] *= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(20, result);
        Assert.AreEqual(20, dictionary["a"]);
    }

    [Test]
    public void TestNewDictionaryElementMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['b'] *= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, dictionary["b"]);
    }

    [Test]
    public void TestExistingListElementMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[0] *= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(20, result);
        Assert.AreEqual(20, list[0]);
    }

    [Test]
    public void TestNewListElementMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[1] *= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, list[1]);
    }

    [Test]
    public void TestArrayElementMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("array[0] *= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(20, result);
        Assert.AreEqual(20, array[0]);
    }

    [Test]
    public void TestObjectMultiplyAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.Property *= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(20, result);
        Assert.AreEqual(20, testObject.Property);
    }

    private class TestObject {
        public int Property { get; set; }
    }

}