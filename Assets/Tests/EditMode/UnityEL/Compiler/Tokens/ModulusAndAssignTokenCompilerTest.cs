using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class ModulusAndAssignTokenCompilerTest : BaseCompilerTest {
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
    public void TestNewPropertyModulusAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("b %= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, evaluator.Properties["b"]);
    }

    [Test]
    public void TestIntegerPropertyModulusAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a %= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, evaluator.Properties["a"]);
    }

    [Test]
    public void TestFloatPropertyModulusAndAssign() {
        UnityELExpression<float> expression = CreateExpression<float>("a %= 5.5");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(4.5f, result);
        Assert.AreEqual(4.5f, evaluator.Properties["a"]);
    }

    [Test]
    public void TestStringPropertyModulusAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a %= '2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, evaluator.Properties["a"]);
    }

    [Test]
    public void TestExistingDictionaryElementModulusAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['a'] %= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, dictionary["a"]);
    }

    [Test]
    public void TestNewDictionaryElementModulusAndAssign() {
        UnityELExpression<float> expression = CreateExpression<float>("dictionary['b'] %= 2");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, dictionary["b"]);
    }

    [Test]
    public void TestExistingListElementModulusAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[0] %= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, list[0]);
    }

    [Test]
    public void TestNewListElementModulusAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[1] %= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, list[1]);
    }

    [Test]
    public void TestArrayElementModulusAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("array[0] %= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, array[0]);
    }

    [Test]
    public void TestObjectModulusAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.Property %= 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(0, result);
        Assert.AreEqual(0, testObject.Property);
    }

    private class TestObject {
        public int Property { get; set; }
    }

}