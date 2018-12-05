using NUnit.Framework;
using System.Collections.Generic;

public class ModulusParserTest {
    private ModulusParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new ModulusParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidModulus() {
        string expression = "1%2";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ModulusToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestModulusWithSpaces() {
        string expression = "1 % 2";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ModulusToken>(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidModulus() {
        string expression = "1+2";
        int pos = 0;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
