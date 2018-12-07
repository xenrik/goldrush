using NUnit.Framework;
using System.Collections.Generic;

public class NotParserTest {
    private NotParser parser;
    
    [SetUp]
    public void Init() {
        parser = new NotParser();
    }

    [Test]
    public void TestValidNot() {
        string expression = "!true";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<NotToken>(result);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestNotWithSpaces() {
        string expression = " !true";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<NotToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestInvalidNot() {
        string expression = "?true";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
