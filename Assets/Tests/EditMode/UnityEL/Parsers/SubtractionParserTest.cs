using NUnit.Framework;
using System.Collections.Generic;

public class SubtractionParserTest {
    private SubtractionParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new SubtractionParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidSubtraction() {
        string expression = "1-2";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<SubtractionToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestSubtractionWithSpaces() {
        string expression = "1 - 2";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<SubtractionToken>(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidSubtraction() {
        string expression = "1+2";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
