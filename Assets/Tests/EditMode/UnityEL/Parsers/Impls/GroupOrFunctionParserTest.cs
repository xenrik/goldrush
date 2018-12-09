using NUnit.Framework;
using System.Collections.Generic;

public class GroupOrFunctionParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private GroupOrFunctionParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new GroupOrFunctionParser();
    }

    [Test]
    public void TestValidGroup() {
        string expression = "(1 + 2)";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new GroupOrFunctionToken(0, rootToken), rootToken[0]);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestGroupWithSpaces() {
        string expression = " (1 + 2)";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new GroupOrFunctionToken(0, rootToken), rootToken[0]);
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

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier(child)";
        int pos = 10;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new GroupOrFunctionToken(10, rootToken), rootToken[0]);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier+child";
        int pos = 10;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(10, pos);
    }
}
