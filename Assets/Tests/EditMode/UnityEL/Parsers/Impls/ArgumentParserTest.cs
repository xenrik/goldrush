using NUnit.Framework;
using System.Collections.Generic;

public class ArgumentParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private ArgumentParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new ArgumentParser();
    }

    [Test]
    public void TestValidAccessor() {
        string expression = "identifier(arg1,arg2)";
        int pos = 15;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ArgumentToken(15, rootToken), rootToken[0]);
        Assert.AreEqual(16, pos);
    }

    [Test]
    public void TestInvalidAccessor() {
        string expression = "identifier(arg1+arg2)";
        int pos = 15;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(15, pos);
    }
}
