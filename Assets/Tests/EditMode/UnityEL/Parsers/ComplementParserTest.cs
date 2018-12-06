using NUnit.Framework;
using System.Collections.Generic;

public class ComplementParserTest {
    private ComplementParser parser;
    
    [SetUp]
    public void Init() {
        parser = new ComplementParser();
    }

    [Test]
    public void TestValidCompliment() {
        string expression = "~0b0110";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ComplementToken>(result);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestComplimentWithSpaces() {
        string expression = " ~0b0110";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ComplementToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestInvalidCompliment() {
        string expression = "?0b0110";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
