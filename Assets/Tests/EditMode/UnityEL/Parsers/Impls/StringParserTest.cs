using NUnit.Framework;
using System.Collections.Generic;

public class StringParserTest : BaseParserTest {
    private StringParser parser;

    [SetUp]
    public void Init() {
        parser = new StringParser();
    }

    [Test]
    public void DoubleQuotedString() {
        InitCompiler("\"123\"", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new StringToken("123", 0), root.Children[0]);
        Assert.AreEqual(5, compiler.Pos);
    }

    [Test]
    public void SingleQuotedString() {
        InitCompiler("'123'", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new StringToken("123", 0), root.Children[0]);
        Assert.AreEqual(5, compiler.Pos);
    }

    [Test]
    public void DoubleQuotedStringWithSpaces() {
        InitCompiler(" \"123\" ", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new StringToken("123", 0), root.Children[0]);
        Assert.AreEqual(6, compiler.Pos);
    }

    [Test]
    public void SingleQuotedStringWithSpaces() {
        InitCompiler(" '123' ", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new StringToken("123", 0), root.Children[0]);
        Assert.AreEqual(6, compiler.Pos);
    }

    [Test]
    public void EmptyString() {
        InitCompiler("", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void UnmatchedToken() {
        InitCompiler("   abc'123'", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void InvalidString() {
        InitCompiler("'abc", 0);

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void EscapedQuotes() {
        InitCompiler("\"abc\\\"def\"", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new StringToken("abc\"def", 0), root.Children[0]);
        Assert.AreEqual(10, compiler.Pos);
    }
}