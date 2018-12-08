using NUnit.Framework;
using System.Collections.Generic;

public class BooleanParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private BooleanParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new BooleanParser();
    }

    [Test]
    public void TrueString() {
        string expression = "true";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new BooleanToken(true, 0, rootToken), rootToken[0]);
        Assert.AreEqual(4, pos);
    }

    [Test]
    public void TrueStringLeadingSpaces() {
        string expression = " true";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new BooleanToken(true, 0, rootToken), rootToken[0]);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void TrueStringWithTrailingChars() {
        string expression = "truestuff";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new BooleanToken(true, 0, rootToken), rootToken[0]);
        Assert.AreEqual(4, pos);
    }

    [Test]
    public void FalseString() {
        string expression = "false";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new BooleanToken(false, 0, rootToken), rootToken[0]);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void FalseStringLeadingSpaces() {
        string expression = " false";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new BooleanToken(false, 0, rootToken), rootToken[0]);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void FalseStringWithTrailingChars() {
        string expression = "falsestuff";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new BooleanToken(false, 0, rootToken), rootToken[0]);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void InvalidStringTrue() {
        string expression = "troo";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void InvalidStringFalse() {
        string expression = "falsy";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void InvalidString() {
        string expression = "blahblahblah";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }
}
