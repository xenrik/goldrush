using NUnit.Framework;
using System.Collections.Generic;

public class PowerParserTest {
    private PowerParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new PowerParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidIntegerPower() {
        string expression = "123^2";
        int pos = 3;
        tokenStack.Push(new IntegerToken(123));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<PowerToken>(result);
        Assert.AreEqual(4, pos);
    }

    [Test]
    public void TestValidDecimalPower() {
        string expression = "123.45^2";
        int pos = 6;
        tokenStack.Push(new DecimalToken(123.45f));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<PowerToken>(result);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void TestInvalidIntegerPower() {
        string expression = "123*2";
        int pos = 3;
        tokenStack.Push(new IntegerToken(123));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidDecimalPower() {
        string expression = "123.45*2";
        int pos = 6;
        tokenStack.Push(new DecimalToken(123.45f));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void TestNoCurrentToken() {
        string expression = "^2";
        int pos = 0;

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void TestNoCurrentInteger() {
        string expression = "'string'^2";
        int pos = 2;
        tokenStack.Push(new StringToken("string"));

        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(8, pos);
    }
}
