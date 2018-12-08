using NUnit.Framework;
using System.Collections.Generic;

public class NotParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private NotParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new NotParser();
    }

    [Test]
    public void TestValidNot() {
        string expression = "!true";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new NotToken(0, rootToken), rootToken[0]);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestNotWithSpaces() {
        string expression = " !true";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new NotToken(0, rootToken), rootToken[0]);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestInvalidNot() {
        string expression = "?true";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }
}
