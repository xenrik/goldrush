using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public abstract class BinaryParserTest<P, T> : BaseParserTest
        where P : TokenParser, new()
        where T : BinaryToken {
    public abstract string ParserSymbol { get; }

    private TokenParser parser;

    [SetUp]
    public void Init() {
        this.parser = new P();
    }

    [Test]
    public void TestValidExpression() {
        string expression = $"1{ParserSymbol}2";
        InitCompiler(expression, 1);
        compiler.Parent.AddChild(new IntegerToken(1, 0));

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(expression.Length, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);

        Assert.IsAssignableFrom<T>(root.Children[0]);

        BinaryToken token = (BinaryToken)root.Children[0];
        Assert.AreEqual(1, token.Position);
        Assert.AreEqual(new IntegerToken(1, 0), token.Lhs);
        Assert.AreEqual(new IntegerToken(2, expression.Length - 1), token.Rhs);
    }

    [Test]
    public void TestExpressionWithSpaces() {
        string expression = $"1 {ParserSymbol} 2";
        InitCompiler(expression, 1);
        compiler.Parent.AddChild(new IntegerToken(1, 0));

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(expression.Length, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);

        Assert.IsAssignableFrom<T>(root.Children[0]);

        BinaryToken token = (BinaryToken)root.Children[0];
        Assert.AreEqual(1, token.Position);
        Assert.AreEqual(new IntegerToken(1, 0), token.Lhs);
        Assert.AreEqual(new IntegerToken(2, expression.Length - 2), token.Rhs);
    }

    [Test]
    public void TestUnknownSymbol() {
        string expression = $"1¬2";
        InitCompiler(expression, 1);
        compiler.Parent.AddChild(new IntegerToken(1, 0));

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreEqual(1, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(1, 0), root.Children[0]);
    }

    [Test]
    public void TestMissingLhs() {
        string expression = $"{ParserSymbol}2";
        InitCompiler(expression, 0);

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(0, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
    }

    [Test]
    public void TestMissingRhs() {
        string expression = $"1{ParserSymbol}";
        InitCompiler(expression, 1);
        compiler.Parent.AddChild(new IntegerToken(1, 0));

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(1, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IntegerToken(1, 0), root.Children[0]);
    }
}