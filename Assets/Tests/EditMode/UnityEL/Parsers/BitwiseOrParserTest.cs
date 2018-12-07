using NUnit.Framework;
using System.Collections.Generic;

public class BitwiseOrParserTest {
    private BitwiseOrParser parser;
    
    [SetUp]
    public void Init() {
        parser = new BitwiseOrParser();
    }

    [Test]
    public void TestValidBitwiseOr() {
        string expression = "0b1010|0b1001";
        int pos = 6;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<BitwiseOrToken>(result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestBitwiseOrWithSpaces() {
        string expression = "0b1010 | 0b1001";
        int pos = 6;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<BitwiseOrToken>(result);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestInvalidBitwiseOr() {
        string expression = "0b1010#0b1001";
        int pos = 6;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(6, pos);
    }
}
