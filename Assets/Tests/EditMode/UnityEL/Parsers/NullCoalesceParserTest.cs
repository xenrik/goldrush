using NUnit.Framework;
using System.Collections.Generic;

public class NullCoalesceParserTest {
    private NullCoalesceParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new NullCoalesceParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidNullCoalesce() {
        string expression = "a??b";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<NullCoalesceToken>(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestNullCoalesceWithSpaces() {
        string expression = "a ?? b";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<NullCoalesceToken>(result);
        Assert.AreEqual(4, pos);
    }

    [Test]
    public void TestInvalidNullCoalesce() {
        string expression = "a !! b";
        int pos = 1;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(1, pos);
    }
}
