using NUnit.Framework;
using System.Collections.Generic;

public class AddAndAssignTokenCompilerTest : BaseCompilerTest {
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
    public void TestNewPropertyIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("b += 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
        Assert.AreEqual(2, evaluator.Properties["b"]);
    }

    [Test]
    public void TestIntegerPropertyIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a += 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(12, result);
        Assert.AreEqual(12, evaluator.Properties["a"]);
    }

    [Test]
    public void TestFloatPropertyIncrementAndAssign() {
        UnityELExpression<float> expression = CreateExpression<float>("a += 10.5");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(20.5f, result);
        Assert.AreEqual(20.5f, evaluator.Properties["a"]);
    }

    [Test]
    public void TestStringPropertyIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a += '2'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(12, result);
        Assert.AreEqual(12, evaluator.Properties["a"]);
    }

    [Test]
    public void TestExistingDictionaryElementIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['a'] += 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(12, result);
        Assert.AreEqual(12, dictionary["a"]);
    }

    [Test]
    public void TestNewDictionaryElementIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("dictionary['b'] += 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
        Assert.AreEqual(2, dictionary["b"]);
    }

    [Test]
    public void TestExistingListElementIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[0] += 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(12, result);
        Assert.AreEqual(12, list[0]);
    }

    [Test]
    public void TestNewListElementIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("list[1] += 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
        Assert.AreEqual(2, list[1]);
    }

    [Test]
    public void TestArrayElementIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("array[0] += 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(12, result);
        Assert.AreEqual(12, array[0]);
    }

    [Test]
    public void TestObjectIncrementAndAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.Property += 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(12, result);
        Assert.AreEqual(12, testObject.Property);
    }

    private class TestObject {
        public int Property { get; set; }
    }

}