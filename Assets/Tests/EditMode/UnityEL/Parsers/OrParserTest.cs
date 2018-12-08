using NUnit.Framework;
using System.Collections.Generic;

public class OrParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private OrParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new OrParser();
    }

    [Test]
    public void TestValidOr() {
        string expression = "true||false";
        int pos = 4;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new OrToken(4, rootToken), rootToken[0]);
        Assert.AreEqual(6, pos);
    }

    [Test]
    public void TestOrWithSpaces() {
        string expression = "true || false";
        int pos = 4;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new OrToken(4, rootToken), rootToken[0]);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestInvalidOr() {
        string expression = "true&&false";
        int pos = 4;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(4, pos);
    }
}
