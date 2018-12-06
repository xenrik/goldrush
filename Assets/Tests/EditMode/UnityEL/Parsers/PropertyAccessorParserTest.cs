using NUnit.Framework;
using System.Collections.Generic;

public class PropertyAccessorParserTest {
    private PropertyAccessorParser parser;
    
    [SetUp]
    public void Init() {
        parser = new PropertyAccessorParser();
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier.child";
        int pos = 10;

        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<PropertyAccessorToken>(result);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier+child";
        int pos = 10;

        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(10, pos);
    }
}
