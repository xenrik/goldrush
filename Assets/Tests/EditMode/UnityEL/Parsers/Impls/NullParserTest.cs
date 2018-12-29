using NUnit.Framework;
using System.Collections.Generic;

public class NullParserTest : BaseParserTest {
    private NullParser parser;

    [SetUp]
    public void Init() {
        parser = new NullParser();
    }

    [Test]
    public void NullString() {
        InitCompiler("null", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new NullToken(0), root.Children[0]);
        Assert.AreEqual(4, compiler.Pos);
    }

    [Test]
    public void NullStringLeadingSpaces() {
        InitCompiler(" null", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new NullToken(0), root.Children[0]);
        Assert.AreEqual(5, compiler.Pos);
    }

    [Test]
    public void NullStringWithTrailingChars() {
        InitCompiler("nullstuff", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void InvalidStringNull() {
        InitCompiler("nuul", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }
}
