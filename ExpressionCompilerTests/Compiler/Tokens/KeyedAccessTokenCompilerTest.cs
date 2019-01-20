using NUnit.Framework;
using System.Collections.Generic;

public class KeyedAccessTokenCompilerTest : BaseCompilerTest {
    private UnityELEvaluator evaluator;
    private TestObject testObject;
    private Dictionary<string, object> testDictionary;
    private TestObject[] testArray;
    private List<TestObject> testList;

    [SetUp]
    public void Init() {
        evaluator = new UnityELEvaluator();

        testObject = new TestObject();
        evaluator.Properties["testObject"] = testObject;

        testDictionary = new Dictionary<string, object>();
        testDictionary["testObject"] = testObject;
        evaluator.Properties["testDictionary"] = testDictionary;

        testArray = new TestObject[1];
        testArray[0] = testObject;
        evaluator.Properties["testArray"] = testArray;

        testList = new List<TestObject>();
        testList.Add(testObject);
        evaluator.Properties["testList"] = testList;
    }

    [Test]
    public void TestPropertyKeyedAccess() {
        UnityELExpression<string> expression = evaluator.Compile<string>("testObject['Property']");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("Value", result);
    }

    [Test]
    public void TestUnknownPropertyKeyedAccess() {
        UnityELExpression<string> expression = evaluator.Compile<string>("testObject['UnknownProperty']");
        Assert.Throws<ParserException>(delegate {
            expression.Evaluate(evaluator);
        }); 
    }

    [Test]
    public void TestChildPropertyKeyedAccess() {
        UnityELExpression<string> expression = evaluator.Compile<string>("testObject.InnerObject['InnerProperty']");
        string result = expression.Evaluate(evaluator);

        Assert.AreEqual("InnerValue", result);
    }

    [Test]
    public void TestDictionaryAccess() {
        UnityELExpression<object> expression = evaluator.Compile<object>("testDictionary['testObject']");
        object result = expression.Evaluate(evaluator);

        Assert.AreEqual(testObject, result);
    }

    [Test]
    public void TestUnknownDictionaryKeyAccess() {
        UnityELExpression<object> expression = evaluator.Compile<object>("testDictionary['unknownKey']");
        object result = expression.Evaluate(evaluator);

        Assert.IsNull(result);
    }

    [Test]
    public void TestArrayAccess() {
        UnityELExpression<object> expression = evaluator.Compile<object>("testArray[0]");
        object result = expression.Evaluate(evaluator);

        Assert.AreEqual(testObject, result);
    }

    [Test]
    public void TestListAccess() {
        UnityELExpression<object> expression = evaluator.Compile<object>("testList[0]");
        object result = expression.Evaluate(evaluator);

        Assert.AreEqual(testObject, result);
    }

    private class TestObject {
        private InnerObject innerObject = new InnerObject();

        public InnerObject InnerObject { get { return innerObject; } }
        public string Property { get { return "Value"; } }
    }

    private class InnerObject {
        public string InnerProperty { get { return "InnerValue"; } }
    }
}