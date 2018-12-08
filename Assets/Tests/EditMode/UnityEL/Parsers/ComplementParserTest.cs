using NUnit.Framework;
using System.Collections.Generic;

public class ComplementParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private ComplementParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new ComplementParser();
    }

    [Test]
    public void TestValidCompliment() {
        string expression = "~0b0110";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ComplementToken(0, rootToken), rootToken[0]);
        Assert.AreEqual(1, pos);
    }

    [Test]
    public void TestComplimentWithSpaces() {
        string expression = " ~0b0110";
        int pos = 0;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ComplementToken(0, rootToken), rootToken[0]);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestInvalidCompliment() {
        string expression = "?0b0110";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }
}
