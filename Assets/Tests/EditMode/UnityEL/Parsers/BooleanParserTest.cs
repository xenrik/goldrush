using NUnit.Framework;
using System.Collections.Generic;

public class BooleanParserTest {
    private BooleanParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new BooleanParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TrueString() {
        string expression = "true";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new BooleanToken(true), result);
        Assert.AreEqual(4, pos);
    }

    [Test]
    public void TrueStringLeadingSpaces() {
        string expression = " true";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new BooleanToken(true), result);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void TrueStringWithTrailingChars() {
        string expression = "truestuff";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new BooleanToken(true), result);
        Assert.AreEqual(4, pos);
    }

    [Test]
    public void FalseString() {
        string expression = "false";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new BooleanToken(false), result);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void FalseStringLeadingSpaces() {
        string expression = " false";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new BooleanToken(false), result);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void FalseStringWithTrailingChars() {
        string expression = "falsestuff";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new BooleanToken(false), result);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void InvalidStringTrue() {
        string expression = "troo";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(null, result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void InvalidStringFalse() {
        string expression = "falsy";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(null, result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void InvalidString() {
        string expression = "blahblahblah";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(null, result);
        Assert.AreEqual(0, pos);
    }
}
