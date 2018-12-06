using NUnit.Framework;
using System.Collections.Generic;

public class CloseParserTest {
    private CloseParser parser;
    
    [SetUp]
    public void Init() {
        parser = new CloseParser();
    }

    [Test]
    public void TestValidCloseFunction() {
        string expression = "function(a)";
        int pos = 10;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<CloseToken>(result);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestValidCloseGroup() {
        string expression = "(a+b)";
        int pos = 4;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<CloseToken>(result);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void TestValidCloseKey() {
        string expression = "property[a]";
        int pos = 10;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<CloseToken>(result);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestValidCloseFunctionWithSpaces() {
        string expression = "function ( a )";
        int pos = 12;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<CloseToken>(result);
        Assert.AreEqual(14, pos);
    }

    [Test]
    public void TestValidCloseGroupWithSpaces() {
        string expression = "( a + b )";
        int pos = 7;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<CloseToken>(result);
        Assert.AreEqual(9, pos);
    }

    [Test]
    public void TestValidCloseKeyWithSpaces() {
        string expression = "property[ a ]";
        int pos = 11;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<CloseToken>(result);
        Assert.AreEqual(13, pos);
    }

    [Test]
    public void TestInvalidCloseFunction() {
        string expression = "function ( a >";
        int pos = 12;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(12, pos);
    }

    [Test]
    public void TestInalidCloseGroup() {
        string expression = "( a + b >";
        int pos = 7;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestInvalidCloseKey() {
        string expression = "property[ a >";
        int pos = 11;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(11, pos);
    }

}
