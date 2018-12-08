using NUnit.Framework;
using System.Collections.Generic;

public class AndParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private AndParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new AndParser();
    }

    [Test]
    public void TestValidAnd() {
        string expression = "true&&false";
        int pos = 4;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new AndToken(4, rootToken), rootToken[0]);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void TestAndWithSpaces() {
        string expression = "true && false";
        int pos = 4;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new AndToken(4, rootToken), rootToken[0]);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestInvalidAnd() {
        string expression = "true!!false";
        int pos = 4;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(4, pos);
    }
}
