using NUnit.Framework;
using System.Collections.Generic;

public class NullCoalesceParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private NullCoalesceParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new NullCoalesceParser();
    }

    [Test]
    public void TestValidNullCoalesce() {
        string expression = "a??b";
        int pos = 1;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new NullCoalesceToken(1, rootToken), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestNullCoalesceWithSpaces() {
        string expression = "a ?? b";
        int pos = 1;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new NullCoalesceToken(1, rootToken), rootToken[0]);
        Assert.AreEqual(4, pos);
    }

    [Test]
    public void TestInvalidNullCoalesce() {
        string expression = "a !! b";
        int pos = 1;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(1, pos);
    }
}
