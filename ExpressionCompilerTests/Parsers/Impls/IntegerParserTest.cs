using NUnit.Framework;
using System.Collections.Generic;

public class IntegerParserTest : BaseParserTest {
    private IntegerParser parser;

    [SetUp]
    public void Init() {
        parser = new IntegerParser();
    }

    [Test]
    public void TestSimpleInteger() {
        InitCompiler("123", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(123, 0), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }

    [Test]
    public void TestMixedInteger() {
        InitCompiler("123abc", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(123, 0), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }

    [Test]
    public void TestSplitIntegerSpace() {
        InitCompiler("123 456", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(123, 0), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }

    [Test]
    public void TestSplitIntegerPeriod() {
        InitCompiler("123.456", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(123, 0), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }

    [Test]
    public void TestLeadingSpace() {
        InitCompiler(" 123", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(123, 0), root.Children[0]);
        Assert.AreEqual(4, compiler.Pos);
    }
}