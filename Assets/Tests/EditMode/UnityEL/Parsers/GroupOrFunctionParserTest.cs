using NUnit.Framework;
using System.Collections.Generic;

public class GroupOrFunctionParserTest {
    private GroupOrFunctionParser parser;
    
    [SetUp]
    public void Init() {
        parser = new GroupOrFunctionParser();
    }

    [Test]
    public void TestValidGroup() {
        string expression = "(1 + 2)";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<GroupToken>(result);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestGroupWithSpaces() {
        string expression = " (1 + 2)";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<GroupToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestInvalidGroup() {
        string expression = "[1 + 2]";
        int pos = 0;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier(child)";
        int pos = 10;

        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<GroupToken>(result);
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
