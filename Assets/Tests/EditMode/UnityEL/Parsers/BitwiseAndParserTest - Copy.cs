using NUnit.Framework;
using System.Collections.Generic;

public class BitwiseAndParserTest {
    private BitwiseAndParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new BitwiseAndParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidBitwiseAnd() {
        string expression = "0b1010&0b1001";
        int pos = 6;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<BitwiseAndToken>(result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestBitwiseAndWithSpaces() {
        string expression = "0b1010 & 0b1001";
        int pos = 6;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<BitwiseAndToken>(result);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestInvalidBitwiseAnd() {
        string expression = "0b1010#0b1001";
        int pos = 6;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(6, pos);
    }
}
