using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ArgumentGroupParserTest : BaseParserTest {
    private ArgumentGroupParser parser;

    [SetUp]
    public void Init() {
        parser = new ArgumentGroupParser();
    }

    [Test]
    public void TestValidStartArgumentGroup() {
        InitCompiler("{a,b} * 2", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(5, compiler.Pos);

        ArgumentGroupToken expected = new ArgumentGroupToken(0);
        expected.Children.Add(new IdentifierToken("a", 1));
        expected.Children.Add(new IdentifierToken("b", 3));
        Assert.AreEqual(expected, root.Children[0]);

        Assert.AreSame(root, compiler.Parent);
    }

    [Test]
    public void TestInvalidStartArgumentGroup() {
        InitCompiler("[a+b} * 2", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }
}