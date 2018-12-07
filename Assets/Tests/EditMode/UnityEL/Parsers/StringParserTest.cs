using NUnit.Framework;
using System.Collections.Generic;

public class StringParserTest {
    private StringParser parser;
    
    [SetUp]
    public void Init() {
        parser = new StringParser();
    }

    [Test]
    public void DoubleQuotedString() {
        string expression = "\"123\"";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new StringToken("123"), result);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void SingleQuotedString() {
        string expression = "'123'";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new StringToken("123"), result);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void DoubleQuotedStringWithSpaces() {
        string expression = "  \"123\" ";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new StringToken("123"), result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void EmptyString() {
        string expression = "";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void UnmatchedToken() {
        string expression = "   abc'123'";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void InvalidString() {
        string expression = "'abc";
        int pos = 0;
        Assert.Throws<ParserException>(delegate {
            parser.Parse(expression.ToCharArray(), ref pos);
        });
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void EscapedQuotes() {
        string expression = "\"abc\\\"def\"";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new StringToken("abc\"def"), result);
        Assert.AreEqual(10, pos);
    }

}
