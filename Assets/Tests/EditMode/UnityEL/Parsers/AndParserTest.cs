using NUnit.Framework;
using System.Collections.Generic;

public class AndParserTest {
    private AndParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new AndParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidAnd() {
        string expression = "true&&false";
        int pos = 4;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<AndToken>(result);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void TestAndWithSpaces() {
        string expression = "true && false";
        int pos = 5;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<AndToken>(result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestInvalidAnd() {
        string expression = "true!!false";
        int pos = 5;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(5, pos);
    }
}
