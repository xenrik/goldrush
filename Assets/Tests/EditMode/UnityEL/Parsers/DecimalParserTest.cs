using NUnit.Framework;
using System.Collections.Generic;

public class DecimalParserTest {
    private DecimalParser parser;

    [SetUp]
    public void Init() {
        parser = new DecimalParser();
    }

    [Test]
    public void TestSimpleInteger() {
        string expression = "123";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(null, result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void TestMixedInteger() {
        string expression = "123abc";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(null, result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void TestSplitIntegerSpace() {
        string expression = "123 456";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(null, result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void TestSimpleDecimal() {
        string expression = "123.456";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new DecimalToken(123.456f), result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestLeadingSpace() {
        string expression = " 123.456";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new DecimalToken(123.456f), result);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestMixedDecimal() {
        string expression = "123.456abc";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new DecimalToken(123.456f), result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestDoubleDecimal() {
        string expression = "123.456.789";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(new DecimalToken(123.456f), result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestInvalidDecimal() {
        string expression = ".789";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.AreEqual(null, result);
        Assert.AreEqual(0, pos);
    }
}
