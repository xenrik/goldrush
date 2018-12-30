using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public abstract class UnaryParserTest<P, T> : BaseParserTest
        where P : TokenParser, new()
        where T : TokenImpl {
    public abstract string ParserSymbol { get; }

    public virtual bool SymbolRequiresTrailingSpace { get { return false; } }

    private TokenParser parser;

    private string FullParserSymbol {
        get {
            if (SymbolRequiresTrailingSpace) {
                return ParserSymbol + " ";
            } else {
                return ParserSymbol;
            }
        }
    }

    [SetUp]
    public void Init() {
        this.parser = new P();
    }

    protected virtual TokenImpl GetRhs(T token) {
        TokenImpl impl = token;
        if (token is UnaryToken) {
            return ((UnaryToken)impl).Rhs;
        } else {
            return null;
        }
    }

    [Test]
    public void TestValidExpression() {
        string expression = $"{FullParserSymbol}2";
        InitCompiler(expression, 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(expression.Length, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);

        Assert.IsAssignableFrom<T>(root.Children[0]);

        T token = (T)root.Children[0];
        Assert.AreEqual(0, token.Position);
        Assert.AreEqual(new IntegerToken(2, expression.Length - 1 -
            (SymbolRequiresTrailingSpace ? 1 : 0)), GetRhs(token));
    }

    [Test]
    public void TestExpressionWithSpaces() {
        string expression = $" {FullParserSymbol} 2";
        InitCompiler(expression, 0);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(expression.Length, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);

        Assert.IsAssignableFrom<T>(root.Children[0]);

        T token = (T)root.Children[0];
        Assert.AreEqual(0, token.Position);
        Assert.AreEqual(new IntegerToken(2, expression.Length - 2 -
            (SymbolRequiresTrailingSpace ? 1 : 0)), GetRhs(token));
    }

    [Test]
    public void TestUnknownSymbol() {
        string expression = $"¬2";
        InitCompiler(expression, 0);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreEqual(0, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
    }

    [Test]
    public void TestMissingRhs() {
        string expression = $"{FullParserSymbol}";
        InitCompiler(expression, 0);

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(0, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(0, root.Children.Count);
    }
}