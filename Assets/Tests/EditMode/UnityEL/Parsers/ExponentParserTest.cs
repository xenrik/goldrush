using NUnit.Framework;
using System.Collections.Generic;

public class ExponentParserTest {
    private ExponentParser parser;
    
    [SetUp]
    public void Init() {
        parser = new ExponentParser();
    }

    [Test]
    public void TestValidIntegerExponent() {
        string expression = "123**2";
        int pos = 3;

        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ExponentToken>(result);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void TestValidDecimalExponent() {
        string expression = "123.45**2";
        int pos = 6;

        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ExponentToken>(result);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestInvalidIntegerExponent() {
        string expression = "123^2";
        int pos = 3;

        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidDecimalExponent() {
        string expression = "123.45^2";
        int pos = 6;

        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(6, pos);
    }
}
