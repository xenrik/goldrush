using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ConditionalOperatorParserTest : BaseParserTest {
    private TokenParser parser;

    [SetUp]
    public void Init() {
        this.parser = new ConditionalOperatorParser();
    }

    [Test]
    public void TestValidExpression() {
        string expression = "true?1:2";
        InitCompiler(expression, 4);

        BooleanToken test = new BooleanToken(true, 0);
        compiler.Parent.AddChild(test);

        Assert.IsTrue(parser.Parse(compiler));
        Assert.AreEqual(expression.Length, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);

        ConditionalOperatorToken expected = new ConditionalOperatorToken(4,
            test, new IntegerToken(1, 5), new IntegerToken(2, 7));
        Assert.AreEqual(expected, root.Children[0]);
    }

    [Test]
    public void TestNotExpression() {
        string expression = "true:1:2";
        InitCompiler(expression, 4);

        BooleanToken test = new BooleanToken(true, 0);
        compiler.Parent.AddChild(test);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreEqual(4, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreSame(test, root.Children[0]);
    }

    [Test]
    public void TestMissingTest() {
        string expression = "?1:2";
        InitCompiler(expression, 0);

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(0, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
    }

    [Test]
    public void TestMissingResultIfTrue() {
        string expression = "true?:2";
        InitCompiler(expression, 4);

        BooleanToken test = new BooleanToken(true, 0);
        compiler.Parent.AddChild(test);

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(4, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreSame(test, root.Children[0]);
    }

    [Test]
    public void TestMissingElse() {
        string expression = "true?1 2";
        InitCompiler(expression, 4);

        BooleanToken test = new BooleanToken(true, 0);
        compiler.Parent.AddChild(test);

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(4, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreSame(test, root.Children[0]);
    }

    [Test]
    public void TestMissingResultIfFalse() {
        string expression = "true?1:";
        InitCompiler(expression, 4);

        BooleanToken test = new BooleanToken(true, 0);
        compiler.Parent.AddChild(test);

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(4, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreSame(test, root.Children[0]);
    }
}