using NUnit.Framework;
using System.Collections.Generic;

public class ComplimentParserTest {
    private ComplementParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new ComplementParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidCompliment() {
        string expression = "~0b0110";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ComplementToken>(result);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestComplimentWithSpaces() {
        string expression = " ~0b0110";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ComplementToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestInvalidCompliment() {
        string expression = "?0b0110";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
