using NUnit.Framework;
using System.Collections.Generic;

public class IntegerParserTest {
    private IntegerParser parser;
    private Stack<Token> tokenStack; 

    [SetUp]
    public void Init() {
        parser = new IntegerParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestSimpleInteger() {
        string expression = "123";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IntegerToken(123), result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestMixedIdentifier() {
        string expression = "123abc";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IntegerToken(123), result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestSplitIdentifierSpace() {
        string expression = "123 456";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IntegerToken(123), result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestSplitIdentifierPeriod() {
        string expression = "123.456";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IntegerToken(123), result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestLeadingSpace() {
        string expression = " 123";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IntegerToken(123), result);
        Assert.AreEqual(4, pos);
    }
}
