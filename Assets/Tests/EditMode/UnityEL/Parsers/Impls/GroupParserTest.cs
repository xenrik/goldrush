using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class GroupParserTest : BaseParserTest {
    private GroupParser parser;

    [SetUp]
    public void Init() {
        parser = new GroupParser();
    }

    [Test]
    public void TestValidStartGroup() {
        InitCompiler("(a+b) * 2", 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new GroupToken(0), root.Children[0]);

        GroupToken groupToken = (GroupToken)root.Children[0];
        Assert.AreSame(groupToken, compiler.Parent);
        Assert.AreEqual(1, compiler.Pos);
    }

    [Test]
    public void TestInvalidStartGroup() {
        InitCompiler("[a+b) * 2", 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
        Assert.AreEqual(0, compiler.Pos);
    }
}