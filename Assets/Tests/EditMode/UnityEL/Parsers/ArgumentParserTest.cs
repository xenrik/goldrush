using NUnit.Framework;
using System.Collections.Generic;

public class ArgumentParserTest {
    private ArgumentParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new ArgumentParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier(arg1,arg2)";
        int pos = 15;
        tokenStack.Push(new IdentifierToken("identifier"));
        tokenStack.Push(new FunctionToken());
        tokenStack.Push(new IdentifierToken("arg1"));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ArgumentToken>(result);
        Assert.AreEqual(16, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier(arg1+arg2)";
        int pos = 15;
        tokenStack.Push(new IdentifierToken("identifier"));
        tokenStack.Push(new FunctionToken());
        tokenStack.Push(new IdentifierToken("arg1"));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(15, pos);
    }
}
