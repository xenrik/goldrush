using NUnit.Framework;
using System.Collections.Generic;

public class IdentifierParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private IdentifierParser parser;

    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new IdentifierParser();
    }

    [Test]
    public void TestSimpleIdentifier() {
        string expression = "abc";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IdentifierToken("abc"), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestMixedIdentifier() {
        string expression = "abc123";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IdentifierToken("abc123"), rootToken[0]);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void TestInvalidIdentifier() {
        string expression = "1abc";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void TestSplitIdentifierSpace() {
        string expression = "abc def";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IdentifierToken("abc"), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestSplitIdentifierPeriod() {
        string expression = "abc.def";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IdentifierToken("abc"), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestLeadingSpace() {
        string expression = " abc";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new IdentifierToken("abc"), rootToken[0]);
        Assert.AreEqual(4, pos);
    }
}
