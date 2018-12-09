using NUnit.Framework;
using System.Collections.Generic;

public class KeyedAccessorParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private KeyedAccessorParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new KeyedAccessorParser();
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier['child']";
        int pos = 10;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new KeyedAccessorToken(10, rootToken), rootToken[0]);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier('child')";
        int pos = 10;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(10, pos);
    }
}
