using NUnit.Framework;
using System.Collections.Generic;

public class StringParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private StringParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new StringParser();
    }

    [Test]
    public void DoubleQuotedString() {
        string expression = "\"123\"";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new StringToken("123", 0, rootToken), rootToken[0]);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void SingleQuotedString() {
        string expression = "'123'";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new StringToken("123", 0, rootToken), rootToken[0]);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void DoubleQuotedStringWithSpaces() {
        string expression = "  \"123\" ";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new StringToken("123", 0, rootToken), rootToken[0]);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void EmptyString() {
        string expression = "";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void UnmatchedToken() {
        string expression = "   abc'123'";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void InvalidString() {
        string expression = "'abc";
        int pos = 0;
        Assert.Throws<ParserException>(delegate {
            parser.Parse(expression.ToCharArray(), ref pos, ref parent);
        });
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void EscapedQuotes() {
        string expression = "\"abc\\\"def\"";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new StringToken("abc\"def", 0, rootToken), rootToken[0]);
        Assert.AreEqual(10, pos);
    }

}
