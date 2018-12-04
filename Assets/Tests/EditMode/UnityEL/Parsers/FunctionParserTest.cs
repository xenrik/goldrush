using NUnit.Framework;
using System.Collections.Generic;

public class FunctionAccessorParserTest {
    private FunctionParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new FunctionParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier(child)";
        int pos = 10;
        tokenStack.Push(new IdentifierToken("identifier"));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<FunctionToken>(result);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier+child";
        int pos = 10;
        tokenStack.Push(new IdentifierToken("identifier"));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(10, pos);
    }

    [Test]
    public void TestNoCurrentToken() {
        string expression = "identifier(child)";
        int pos = 10;

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(10, pos);
    }

    [Test]
    public void TestNoCurrentIdentifier() {
        string expression = "'string'(child)";
        int pos = 8;
        tokenStack.Push(new StringToken("string"));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(8, pos);
    }
}
