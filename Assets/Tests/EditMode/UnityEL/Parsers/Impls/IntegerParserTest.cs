using NUnit.Framework;
using System.Collections.Generic;

public class IntegerParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private IntegerParser parser;

    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new IntegerParser();
    }

    [Test]
    public void TestSimpleInteger() {
        string expression = "123";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IntegerToken(123, 0, rootToken), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestMixedIdentifier() {
        string expression = "123abc";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IntegerToken(123, 0, rootToken), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestSplitIdentifierSpace() {
        string expression = "123 456";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IntegerToken(123, 0, rootToken), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestSplitIdentifierPeriod() {
        string expression = "123.456";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IntegerToken(123, 0, rootToken), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestLeadingSpace() {
        string expression = " 123";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IntegerToken(123, 0, rootToken), rootToken[0]);
        Assert.AreEqual(4, pos);
    }
}
