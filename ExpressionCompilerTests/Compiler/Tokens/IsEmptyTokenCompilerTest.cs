using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class IsEmptyTokenCompilerTest : BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public UnityELExpression<T> CreateExpression<T>(string expression) {
        evaluator = new UnityELEvaluator();
        evaluator.Properties["emptyString"] = "";
        evaluator.Properties["string"] = "abc";
        evaluator.Properties["emptyArray"] = new string[0];
        evaluator.Properties["array"] = new string[] { "abc" };
        evaluator.Properties["emptyList"] = new List<string>();
        evaluator.Properties["list"] = new List<string> { "abc" };
        evaluator.Properties["emptyDictionary"] = new Dictionary<string, string>();
        evaluator.Properties["dictionary"] = new Dictionary<string, string> {
            { "abc", "123" }
        };
        evaluator.Properties["emptySet"] = new HashSet<string>();
        evaluator.Properties["set"] = new HashSet<string> { "abc" };
        evaluator.Properties["emptyEnumerable"] = new TestObject(false);
        evaluator.Properties["enumerable"] = new TestObject(true);
        evaluator.Properties["emptyOther"] = null;
        evaluator.Properties["other"] = new OtherObject();

        return evaluator.Compile<T>(expression);
    }

    [Test]
    public void TestEmptyStringConstantIsEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty ''");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestEmptyArrayIsEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty emptyArray");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestLoadedArrayIsNotEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty array");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestEmptyListIsEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty emptyList");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestLoadedListIsNotEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty list");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestEmptyDictionaryIsEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty emptyDictionary");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestLoadedDictionaryIsNotEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty dictionary");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestEmptySetIsEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty emptySet");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestLoadedSetIsNotEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty set");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestEmptyEnumerableIsEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty emptyEnumerable");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestLoadedEnumerableIsNotEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty enumerable");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    [Test]
    public void TestEmptyOtherIsEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty emptyOther");
        bool result = expression.Evaluate(evaluator);

        Assert.IsTrue(result);
    }

    [Test]
    public void TestLoadedOtherIsNotEmpty() {
        UnityELExpression<bool> expression = CreateExpression<bool>("empty other");
        bool result = expression.Evaluate(evaluator);

        Assert.IsFalse(result);
    }

    private class TestObject : IEnumerable<string> {
        private List<string> innerList = new List<string>();

        public TestObject(bool loadList) {
            if (loadList) {
                innerList.Add("abc");
            }
        }

        public IEnumerator<string> GetEnumerator() {
            return innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return innerList.GetEnumerator();
        }
    }

    private class OtherObject {
    }
}