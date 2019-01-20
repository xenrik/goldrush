using NUnit.Framework;
using System.Collections.Generic;

public class AssignTokenCompilerTest : BaseCompilerTest {
    private UnityELEvaluator evaluator;

    private Dictionary<string, string> stringDictionary;
    private List<string> stringList;
    private string[] stringArray;

    private Dictionary<int, int> intDictionary;
    private List<int> intList;
    private int[] intArray;

    private TestObject testObject;

    private UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();

        stringDictionary = new Dictionary<string, string>();
        evaluator.Properties["stringDictionary"] = stringDictionary;

        stringList = new List<string>();
        evaluator.Properties["stringList"] = stringList;

        stringArray = new string[1];
        evaluator.Properties["stringArray"] = stringArray;

        intDictionary = new Dictionary<int, int>();
        evaluator.Properties["intDictionary"] = intDictionary;

        intList = new List<int>();
        evaluator.Properties["intList"] = intList;

        intArray = new int[1];
        evaluator.Properties["intArray"] = intArray;

        testObject = new TestObject();
        evaluator.Properties["testObject"] = testObject;

        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestIntegerPropertyAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("a = 2");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(2, result);
        Assert.AreEqual(2, evaluator.Properties["a"]);
    }

    [Test]
    public void TestFloatPropertyAssign() {
        UnityELExpression<float> expression = CreateExpression<float>("a = 10.5");
        float result = expression.Evaluate(evaluator);

        Assert.AreEqual(10.5f, result);
        Assert.AreEqual(10.5, evaluator.Properties["a"]);
    }

    [Test]
    public void TestStringPropertyAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("a = 'abc'");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("abc", result);
        Assert.AreEqual("abc", evaluator.Properties["a"]);
    }

    [Test]
    public void TestStringDictionaryStringAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("stringDictionary['a'] = 'abc'");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("abc", result);
        Assert.AreEqual("abc", stringDictionary["a"]);
    }

    [Test]
    public void TestStringDictionaryIntegerAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("stringDictionary['a'] = 123");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("123", result);
        Assert.AreEqual("123", stringDictionary["a"]);
    }

    [Test]
    public void TestStringListStringAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("stringList[1] = 'abc'");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("abc", result);
        Assert.AreEqual("abc", stringList[1]);
    }

    [Test]
    public void TestStringListIntegerAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("stringList[1] = 123");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("123", result);
        Assert.AreEqual("123", stringList[1]);
    }

    [Test]
    public void TestStringArrayStringAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("stringArray[0] = 'abc'");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("abc", result);
        Assert.AreEqual("abc", stringArray[0]);
    }

    [Test]
    public void TestStringArrayIntegerAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("stringArray[0] = 123");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("123", result);
        Assert.AreEqual("123", stringArray[0]);
    }

    [Test]
    public void TestIntDictionaryStringAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("intDictionary[123] = '123'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(123, result);
        Assert.AreEqual(123, intDictionary[123]);
    }

    [Test]
    public void TestIntDictionaryIntegerAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("intDictionary[123] = 123");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(123, result);
        Assert.AreEqual(123, intDictionary[123]);
    }

    [Test]
    public void TestIntListStringAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("intList[1] = '123'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(123, result);
        Assert.AreEqual(123, intList[1]);
    }

    [Test]
    public void TestIntListIntegerAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("intList[1] = 123");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(123, result);
        Assert.AreEqual(123, intList[1]);
    }

    [Test]
    public void TestIntArrayStringAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("intArray[0] = '123'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(123, result);
        Assert.AreEqual(123, intArray[0]);
    }

    [Test]
    public void TestIntArrayIntegerAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("intArray[0] = 123");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(123, result);
        Assert.AreEqual(123, intArray[0]);
    }

    [Test]
    public void TestObjectStringStringAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("testObject.StringProperty = 'abc'");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("abc", result);
        Assert.AreEqual("abc", testObject.StringProperty);
    }

    [Test]
    public void TestObjectStringIntegerAssign() {
        UnityELExpression<string> expression = CreateExpression<string>("testObject.StringProperty = 123");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("123", result);
        Assert.AreEqual("123", testObject.StringProperty);
    }

    [Test]
    public void TestObjectIntegerStringAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.IntegerProperty = '123'");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(123, result);
        Assert.AreEqual(123, testObject.IntegerProperty);
    }

    [Test]
    public void TestObjectIntegerIntegerAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.IntegerProperty = 123");
        int result = expression.Evaluate(evaluator);

        Assert.AreEqual(123, result);
        Assert.AreEqual(123, testObject.IntegerProperty);
    }

    [Test]
    public void TestObjectReadOnlyAssign() {
        UnityELExpression<int> expression = CreateExpression<int>("testObject.ReadOnlyProperty = 123");
        Assert.Throws<ParserException>(delegate {
            expression.Evaluate(evaluator);
        });
    }

    private class TestObject {
        public string StringProperty { get; set; }
        public int IntegerProperty { get; set; }

        public string ReadOnlyProperty { get; private set; }
    }

}