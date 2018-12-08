using NUnit.Framework;
using System.Collections.Generic;

public class ExponentParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private ExponentParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new ExponentParser();
    }

    [Test]
    public void TestValidIntegerExponent() {
        string expression = "123**2";
        int pos = 3;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ExponentToken(3, rootToken), rootToken[0]);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void TestValidDecimalExponent() {
        string expression = "123.45**2";
        int pos = 6;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ExponentToken(6, rootToken), rootToken[0]);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestInvalidIntegerExponent() {
        string expression = "123^2";
        int pos = 3;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidDecimalExponent() {
        string expression = "123.45^2";
        int pos = 6;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(6, pos);
    }
}
