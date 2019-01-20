using NUnit.Framework;
using System.Collections.Generic;

public class BooleanParserTest : BaseParserTest {
    private BooleanParser parser;

    [SetUp]
    public void Init() {
        parser = new BooleanParser();
    }

    [Test]
    public void TrueString() {
        InitCompiler("true", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new BooleanToken(true, 0), root.Children[0]);
        Assert.AreEqual(4, compiler.Pos);
    }

    [Test]
    public void TrueStringLeadingSpaces() {
        InitCompiler(" true", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new BooleanToken(true, 0), root.Children[0]);
        Assert.AreEqual(5, compiler.Pos);
    }

    [Test]
    public void TrueStringWithTrailingChars() {
        InitCompiler("truestuff", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void FalseString() {
        InitCompiler("false", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new BooleanToken(false, 0), root.Children[0]);
        Assert.AreEqual(5, compiler.Pos);
    }

    [Test]
    public void FalseStringLeadingSpaces() {
        InitCompiler(" false", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new BooleanToken(false, 0), root.Children[0]);
        Assert.AreEqual(6, compiler.Pos);
    }

    [Test]
    public void FalseStringWithTrailingChars() {
        InitCompiler("falsestuff", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void InvalidStringTrue() {
        InitCompiler("troo", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void InvalidStringFalse() {
        InitCompiler("falsy", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void InvalidString() {
        InitCompiler("blahblahblah", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }
}
