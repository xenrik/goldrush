using NUnit.Framework;
using System.Collections.Generic;

public class HexIntegerParserTest : BaseParserTest {
    private HexIntegerParser parser;

    [SetUp]
    public void Init() {
        parser = new HexIntegerParser();
    }

    [Test]
    public void TestSimpleHexInteger() {
        InitCompiler("0x0A0", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(160, 0, 16), root.Children[0]);
        Assert.AreEqual(5, compiler.Pos);
    }
    
    [Test]
    public void TestInvalidHexInteger() {
        InitCompiler("0b010", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }

    [Test]
    public void TestBadHexInteger() {
        InitCompiler("0x0Q0", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(0, 0, 16), root.Children[0]);
        Assert.AreEqual(3, compiler.Pos);
    }
}
