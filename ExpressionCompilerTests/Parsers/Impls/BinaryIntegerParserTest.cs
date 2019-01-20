using NUnit.Framework;
using System.Collections.Generic;

public class BinaryIntegerParserTest : BaseParserTest {
    private BinaryIntegerParser parser;

    [SetUp]
    public void Init() {
        parser = new BinaryIntegerParser();
    }

    [Test]
    public void TestSimpleBinaryInteger() {
        InitCompiler("0b010", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(2, 0, 2), root.Children[0]);
        Assert.AreEqual(5, compiler.Pos);
    }

    [Test]
    public void TestInvalidBinaryInteger() {
        InitCompiler("0x010", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void TestBadBinaryInteger() {
        InitCompiler("0b0C0", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(0, 0, 2), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }
}
