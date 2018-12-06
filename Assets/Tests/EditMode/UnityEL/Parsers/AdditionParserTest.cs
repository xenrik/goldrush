using NUnit.Framework;
using System.Collections.Generic;

public class AdditionParserTest {
    private AdditionParser parser;
    
    [SetUp]
    public void Init() {
        parser = new AdditionParser();
    }

    [Test]
    public void TestValidAddition() {
        string expression = "1+2";
        int pos = 1;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<AdditionToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestAdditionWithSpaces() {
        string expression = "1 + 2";
        int pos = 1;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<AdditionToken>(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidAddition() {
        string expression = "1-2";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
