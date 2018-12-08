using NUnit.Framework;
using System.Collections.Generic;

public class BitwiseAndParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private BitwiseAndParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new BitwiseAndParser();
    }

    [Test]
    public void TestValidBitwiseAnd() {
        string expression = "0b1010&0b1001";
        int pos = 6;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new BitwiseAndToken(6, rootToken), rootToken[0]);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestBitwiseAndWithSpaces() {
        string expression = "0b1010 & 0b1001";
        int pos = 6;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new BitwiseAndToken(6, rootToken), rootToken[0]);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestInvalidBitwiseAnd() {
        string expression = "0b1010#0b1001";
        int pos = 6;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(6, pos);
    }
}
