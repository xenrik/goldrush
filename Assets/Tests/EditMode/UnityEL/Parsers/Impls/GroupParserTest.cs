using NUnit.Framework;
using System.Collections.Generic;

public class GroupParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private GroupParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new GroupParser();
    }

    [Test]
    public void TestValidGroup() {
        string expression = "(1 + 2)";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new GroupToken(0, rootToken), rootToken[0]);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestGroupWithSpaces() {
        string expression = " (1 + 2)";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new GroupToken(0, rootToken), rootToken[0]);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestInvalidGroup() {
        string expression = "[1 + 2]";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }
}
