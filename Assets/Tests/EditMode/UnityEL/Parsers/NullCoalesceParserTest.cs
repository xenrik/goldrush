using NUnit.Framework;
using System.Collections.Generic;

public class NullCoalesceParserTest {
    private NullCoalesceParser parser;
    
    [SetUp]
    public void Init() {
        parser = new NullCoalesceParser();
    }

    [Test]
    public void TestValidNullCoalesce() {
        string expression = "a??b";
        int pos = 1;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<NullCoalesceToken>(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestNullCoalesceWithSpaces() {
        string expression = "a ?? b";
        int pos = 1;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<NullCoalesceToken>(result);
        Assert.AreEqual(4, pos);
    }

    [Test]
    public void TestInvalidNullCoalesce() {
        string expression = "a !! b";
        int pos = 1;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(1, pos);
    }
}
