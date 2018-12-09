using NUnit.Framework;
using System.Collections.Generic;

public class CloseParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private CloseParser parser;

    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parser = new CloseParser();
    }

    [Test]
    public void TestValidCloseFunction() {
        FunctionToken functionToken = new FunctionToken(0, rootToken);
        parent = functionToken;

        string expression = "function(a)";
        int pos = 10;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, functionToken.ChildCount);
        Assert.AreEqual(new CloseToken(10, functionToken), functionToken[0]);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestValidCloseGroup() {
        GroupToken groupToken = new GroupToken(0, rootToken);
        parent = groupToken;

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
        KeyedAccessorToken keyToken = new KeyedAccessorToken(0, rootToken);
        parent = keyToken;

        string expression = "property[a]";
        int pos = 10;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, keyToken.ChildCount);
        Assert.AreEqual(new CloseToken(10, keyToken), keyToken[0]);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestValidCloseFunctionWithSpaces() {
        FunctionToken functionToken = new FunctionToken(0, rootToken);
        parent = functionToken;

        string expression = "function ( a )";
        int pos = 12;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, functionToken.ChildCount);
        Assert.AreEqual(new CloseToken(12, functionToken), functionToken[0]);
        Assert.AreEqual(14, pos);
    }

    [Test]
    public void TestValidCloseGroupWithSpaces() {
        GroupToken groupToken = new GroupToken(0, rootToken);
        parent = groupToken;

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
        KeyedAccessorToken keyToken = new KeyedAccessorToken(0, rootToken);
        parent = keyToken;

        string expression = "property[ a ]";
        int pos = 11;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        // Parent starts as Group, but should change to Root
        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, keyToken.ChildCount);
        Assert.AreEqual(new CloseToken(11, keyToken), keyToken[0]);
        Assert.AreEqual(13, pos);
    }

    [Test]
    public void TestInvalidCloseFunction() {
        FunctionToken functionToken = new FunctionToken(0, rootToken);
        parent = functionToken;

        string expression = "function ( a >";
        int pos = 12;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(functionToken, parent);
        Assert.AreEqual(0, functionToken.ChildCount);
        Assert.AreEqual(12, pos);
    }

    [Test]
    public void TestInvalidCloseGroup() {
        GroupToken groupToken = new GroupToken(0, rootToken);
        parent = groupToken;

        string expression = "( a + b >";
        int pos = 7;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(groupToken, parent);
        Assert.AreEqual(0, groupToken.ChildCount);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestInvalidCloseKey() {
        KeyedAccessorToken keyToken = new KeyedAccessorToken(0, rootToken);
        parent = keyToken;

        string expression = "property[ a >";
        int pos = 11;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(keyToken, parent);
        Assert.AreEqual(0, keyToken.ChildCount);
        Assert.AreEqual(11, pos);
    }

}
