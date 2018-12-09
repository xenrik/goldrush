using NUnit.Framework;
using System.Collections.Generic;

public class ConditionalElseParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private ConditionalElseParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new ConditionalElseParser();
    }

    [Test]
    public void TestValidConditionalOperator() {
        string expression = "(true)?1:2";
        int pos = 8;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ConditionalElseToken(6, rootToken), rootToken[0]);
        Assert.AreEqual(9, pos);
    }

    [Test]
    public void TestConditionalOperatorWithSpaces() {
        string expression = "(true) ? 1 : 2";
        int pos = 10;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ConditionalElseToken(6, rootToken), rootToken[0]);
        Assert.AreEqual(11, pos);
    }

    [Test]
    public void TestInvalidConditionalOperator() {
        string expression = "(true) ! 2 ? 1";
        int pos = 10;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(10, pos);
    }
}
