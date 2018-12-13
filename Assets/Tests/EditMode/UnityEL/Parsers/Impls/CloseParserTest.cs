using UnityEngine;
using UnityEditor;
using NUnit.Framework;

/**
 * We test a few common scenarios for closing here. Individual parser tests
 * should prove themselves they support closing
 */
public class CloseParserTest : BaseParserTest {
    private CloseParser parser;

    [SetUp]
    public void Init() {
        parser = new CloseParser(')');
    }

    [Test]
    public void ValidClose() {
        InitCompiler("(a+b)", 4);
        GroupToken group = new GroupToken();
        compiler.ParentTokens.Push(group);

        Assert.IsTrue(parser.Parse(compiler));

        // The parent should return to the root after closing
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(5, compiler.Pos);

        Assert.IsTrue(group.IsClosed);
    }

    [Test]
    public void InvalidClose() {
        InitCompiler("(a+b]", 4);
        GroupToken group = new GroupToken();
        compiler.ParentTokens.Push(group);

        Assert.IsFalse(parser.Parse(compiler));

        // The parent should still be the group
        Assert.AreSame(group, compiler.Parent);
        Assert.AreEqual(4, compiler.Pos);

        Assert.IsFalse(group.IsClosed);
    }

    [Test]
    public void NotClosable() {
        InitCompiler("b)", 1);
        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        // The parent should still be the group
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, compiler.Pos);
    }

    [Test]
    public void AlreadyClosed() {
        InitCompiler("(a+b))", 5);
        GroupToken group = new GroupToken();
        group.Close();
        compiler.ParentTokens.Push(group);

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        // The parent should still be the group
        Assert.AreSame(group, compiler.Parent);
        Assert.AreEqual(5, compiler.Pos);
    }

}