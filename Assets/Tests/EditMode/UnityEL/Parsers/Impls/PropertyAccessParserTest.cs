using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PropertyAccessParserTest : BaseParserTest {
    private TokenParser parser;

    [SetUp]
    public void Init() {
        this.parser = new PropertyAccessParser();
    }

    [Test]
    public void TestValidExpression() {
        string expression = $"host.property";
        InitCompiler(expression, 4);
        compiler.Parent.AddChild(new IdentifierToken("host", 0));

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(expression.Length, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new PropertyAccessToken(4, 
                new IdentifierToken("host", 0), 
                new IdentifierToken("property", 5)),
            root.Children[0]);
    }

    [Test]
    public void TestExpressionWithSpaces() {
        string expression = $"host . property";
        InitCompiler(expression, 4);
        compiler.Parent.AddChild(new IdentifierToken("host", 0));

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(expression.Length, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new PropertyAccessToken(4, 
                new IdentifierToken("host", 0), 
                new IdentifierToken("property", 6)),
            root.Children[0]);
    }

    [Test]
    public void TestUnknownSymbol() {
        string expression = $"host¬property";
        InitCompiler(expression, 4);
        compiler.Parent.AddChild(new IdentifierToken("host", 0));

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreEqual(4, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IdentifierToken("host", 0), root.Children[0]);
    }

    [Test]
    public void TestMissingLhs() {
        string expression = $".2";
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
        string expression = $"host.";
        InitCompiler(expression, 4);
        compiler.Parent.AddChild(new IdentifierToken("host", 0));

        Assert.Throws<ParserException>(delegate {
            parser.Parse(compiler);
        });

        Assert.AreEqual(4, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new IdentifierToken("host", 0), root.Children[0]);
    }

    [Test]
    public void TestThreeLevelExpression() {
        string expression = $"host.property.decendent";
        InitCompiler(expression, 13);
        compiler.Parent.AddChild(new PropertyAccessToken(4,
            new IdentifierToken("host", 0),
            new IdentifierToken("property", 5)));

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(expression.Length, compiler.Pos);
        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new PropertyAccessToken(13,
            // LHS
            new PropertyAccessToken(4,
                new IdentifierToken("host", 0),
                new IdentifierToken("property", 5)),
            // RHS
            new IdentifierToken("decendent", 14)
            ),
            root.Children[0]);
    }
}