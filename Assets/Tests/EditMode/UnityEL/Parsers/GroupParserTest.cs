using NUnit.Framework;
using System.Collections.Generic;

public class GroupParserTest {
    private GroupParser parser;
    
    [SetUp]
    public void Init() {
        parser = new GroupParser();
    }

    [Test]
    public void TestValidGroup() {
        string expression = "(1 + 2)";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<GroupToken>(result);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestGroupWithSpaces() {
        string expression = " (1 + 2)";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<GroupToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestInvalidGroup() {
        string expression = "[1 + 2]";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
