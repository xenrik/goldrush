using NUnit.Framework;
using System.Collections.Generic;

public class CloseParserTest {
    private GroupToken groupToken;
    private RootToken rootToken;
    private RawToken parent;
    private CloseParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();

        groupToken = new GroupToken(0, rootToken);
        parent = groupToken;

        parser = new CloseParser();
    }

    [Test]
    public void TestValidCloseFunction() {
        string expression = "function(a)";
        int pos = 10;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, groupToken.ChildCount);
        Assert.AreEqual(new CloseToken(10, groupToken), groupToken[0]);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestValidCloseGroup() {
        string expression = "(a+b)";
        int pos = 4;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, groupToken.ChildCount);
        Assert.AreEqual(new CloseToken(4, groupToken), groupToken[0]);
        Assert.AreEqual(5, pos);
    }

    [Test]
    public void TestValidCloseKey() {
        string expression = "property[a]";
        int pos = 10;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, groupToken.ChildCount);
        Assert.AreEqual(new CloseToken(10, groupToken), groupToken[0]);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestValidCloseFunctionWithSpaces() {
        string expression = "function ( a )";
        int pos = 12;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, groupToken.ChildCount);
        Assert.AreEqual(new CloseToken(12, groupToken), groupToken[0]);
        Assert.AreEqual(14, pos);
    }

    [Test]
    public void TestValidCloseGroupWithSpaces() {
        string expression = "( a + b )";
        int pos = 7;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, groupToken.ChildCount);
        Assert.AreEqual(new CloseToken(7, groupToken), groupToken[0]);
        Assert.AreEqual(9, pos);
    }

    [Test]
    public void TestValidCloseKeyWithSpaces() {
        string expression = "property[ a ]";
        int pos = 11;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, groupToken.ChildCount);
        Assert.AreEqual(new CloseToken(11, groupToken), groupToken[0]);
        Assert.AreEqual(13, pos);
    }

    [Test]
    public void TestInvalidCloseFunction() {
        string expression = "function ( a >";
        int pos = 12;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(groupToken, parent);
        Assert.AreEqual(0, groupToken.ChildCount);
        Assert.AreEqual(12, pos);
    }

    [Test]
    public void TestInvalidCloseGroup() {
        string expression = "( a + b >";
        int pos = 7;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(groupToken, parent);
        Assert.AreEqual(0, groupToken.ChildCount);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestInvalidCloseKey() {
        string expression = "property[ a >";
        int pos = 11;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(groupToken, parent);
        Assert.AreEqual(0, groupToken.ChildCount);
        Assert.AreEqual(11, pos);
    }

}
