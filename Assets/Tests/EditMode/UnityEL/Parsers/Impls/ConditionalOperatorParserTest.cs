using NUnit.Framework;
using System.Collections.Generic;

public class ConditionalOperatorParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private ConditionalOperatorParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new ConditionalOperatorParser();
    }

    [Test]
    public void TestValidConditionalOperator() {
        string expression = "(true)?1:2";
        int pos = 6;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ConditionalOperatorToken(6, rootToken), rootToken[0]);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestConditionalOperatorWithSpaces() {
        string expression = "(true) ? 1 : 2";
        int pos = 6;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new ConditionalOperatorToken(6, rootToken), rootToken[0]);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestInvalidConditionalOperator() {
        string expression = "(true) ! 2 ? 1";
        int pos = 6;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(6, pos);
    }
}
