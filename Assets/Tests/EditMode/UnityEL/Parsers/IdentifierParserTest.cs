using NUnit.Framework;
using System.Collections.Generic;

public class IdentifierParserTest {
    private IdentifierParser parser;

    [SetUp]
    public void Init() {
        parser = new IdentifierParser();
    }

    [Test]
    public void TestSimpleIdentifier() {
        string expression = "abc";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IdentifierToken("abc"), result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestMixedIdentifier() {
        string expression = "abc123";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IdentifierToken("abc123"), result);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void TestInvalidIdentifier() {
        string expression = "1abc";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void TestSplitIdentifierSpace() {
        string expression = "abc def";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IdentifierToken("abc"), result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestSplitIdentifierPeriod() {
        string expression = "abc.def";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IdentifierToken("abc"), result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestLeadingSpace() {
        string expression = " abc";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new IdentifierToken("abc"), result);
        Assert.AreEqual(4, pos);
    }
}
