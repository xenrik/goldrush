using NUnit.Framework;
using System.Collections.Generic;

public class DecimalParserTest : BaseParserTest {
    private DecimalParser parser;

    [SetUp]
    public void Init() {
        parser = new DecimalParser();
    }

    [Test]
    public void TestSimpleInteger() {
        InitCompiler("123", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void TestMixedInteger() {
        InitCompiler("123abc", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void TestSplitIntegerSpace() {
        InitCompiler("123 456", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void TestSimpleDecimal() {
        InitCompiler("123.456", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new DecimalToken(123.456f, 0), root.Children[0]);
        Assert.AreEqual(7, compiler.Pos);
    }

    [Test]
    public void TestLeadingSpace() {
        InitCompiler(" 123.456", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new DecimalToken(123.456f, 0), root.Children[0]);
        Assert.AreEqual(8, compiler.Pos);
    }

    [Test]
    public void TestMixedDecimal() {
        InitCompiler("123.456abc", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new DecimalToken(123.456f, 0), root.Children[0]);
        Assert.AreEqual(7, compiler.Pos);
    }

    [Test]
    public void TestDoubleDecimal() {
        InitCompiler("123.456.789", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new DecimalToken(123.456f, 0), root.Children[0]);
        Assert.AreEqual(7, compiler.Pos);
    }

    [Test]
    public void TestInvalidDecimal() {
        InitCompiler(".789", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }
}
