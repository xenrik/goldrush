using NUnit.Framework;
using System.Collections.Generic;

public class FunctionAccessorParserTest {
    private FunctionParser parser;
    
    [SetUp]
    public void Init() {
        parser = new FunctionParser();
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier(child)";
        int pos = 10;

        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<FunctionToken>(result);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier+child";
        int pos = 10;

        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(10, pos);
    }
}
