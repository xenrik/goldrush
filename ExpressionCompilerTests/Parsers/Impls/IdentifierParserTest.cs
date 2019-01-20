using NUnit.Framework;
using System.Collections.Generic;

public class IdentifierParserTest : BaseParserTest {
    private IdentifierParser parser;

    [SetUp]
    public void Init() {
        parser = new IdentifierParser();
    }

    [Test]
    public void TestSimpleIdentifier() {
        InitCompiler("abc", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IdentifierToken("abc", 0), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }

    [Test]
    public void TestMixedIdentifier() {
        InitCompiler("abc123", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IdentifierToken("abc123", 0), root.Children[0]);
        Assert.AreEqual(6, compiler.Pos);
    }

    [Test]
    public void TestInvalidIdentifier() {
        InitCompiler("1abc", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void TestSplitIdentifierSpace() {
        InitCompiler("abc def", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IdentifierToken("abc", 0), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }

    [Test]
    public void TestSplitIdentifierPeriod() {
        InitCompiler("abc.def", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IdentifierToken("abc", 0), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }

    [Test]
    public void TestLeadingSpace() {
        InitCompiler(" abc", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IdentifierToken("abc", 0), root.Children[0]);
        Assert.AreEqual(4, compiler.Pos);
    }
}