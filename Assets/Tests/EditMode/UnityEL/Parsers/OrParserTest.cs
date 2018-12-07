using NUnit.Framework;
using System.Collections.Generic;

public class OrParserTest {
    private OrParser parser;
    
    [SetUp]
    public void Init() {
        parser = new OrParser();
    }

    [Test]
    public void TestValidOr() {
        string expression = "true||false";
        int pos = 4;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<OrToken>(result);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void TestOrWithSpaces() {
        string expression = "true || false";
        int pos = 5;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<OrToken>(result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestInvalidOr() {
        string expression = "true&&false";
        int pos = 5;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(5, pos);
    }
}
