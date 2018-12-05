using NUnit.Framework;
using System.Collections.Generic;

public class MultiplicationParserTest {
    private MultiplicationParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new MultiplicationParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidMultiplication() {
        string expression = "1*2";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<MultiplicationToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestMultiplicationWithSpaces() {
        string expression = "1 * 2";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<MultiplicationToken>(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidMultiplication() {
        string expression = "1/2";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
