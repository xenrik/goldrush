using NUnit.Framework;
using System.Collections.Generic;

public class KeyedAccessorParserTest {
    private KeyedAccessorParser parser;
    
    [SetUp]
    public void Init() {
        parser = new KeyedAccessorParser();
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier['child']";
        int pos = 10;

        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<KeyedAccessorToken>(result);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier('child')";
        int pos = 10;

        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(10, pos);
    }
}
