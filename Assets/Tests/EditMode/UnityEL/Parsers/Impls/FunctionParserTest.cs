using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class FunctionParserTest : BaseParserTest {
    private FunctionParser parser;

    [SetUp]
    public void Init() {
        parser = new FunctionParser();
    }

    [Test]
    public void TestValidStartFunction() {
        InitCompiler("function(a+b) * 2", 8);
        IdentifierToken functionName = new IdentifierToken("function", 0);
        compiler.Parent.AddChild(functionName);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(13, compiler.Pos);
        Assert.AreEqual(1, root.Children.Count);

        FunctionToken expected = new FunctionToken(8, functionName);
        expected.Children.Add(new AdditionToken(10,
            new IdentifierToken("a", 9), new IdentifierToken("b", 11)));
        Assert.AreEqual(expected, root.Children[0]);

        Assert.AreSame(root, compiler.Parent);
    }

    [Test]
    public void TestInvalidStartFunction() {
        InitCompiler("function[a+b) * 2", 8);
        IdentifierToken functionName = new IdentifierToken("function", 0);
        compiler.Parent.AddChild(functionName);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreSame(functionName, root.Children[0]);
        Assert.AreEqual(8, compiler.Pos);
    }

    [Test]
    public void TestValidStartMemberFunction() {
        InitCompiler("host.function(a+b) * 2", 13);
        PropertyAccessToken propertyAccess = new PropertyAccessToken(4,
            new IdentifierToken("host", 0),
            new IdentifierToken("function", 5));
        compiler.Parent.AddChild(propertyAccess);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(18, compiler.Pos);
        Assert.AreEqual(1, root.Children.Count);

        FunctionToken expected = new FunctionToken(13, propertyAccess);
        expected.Children.Add(new AdditionToken(15,
            new IdentifierToken("a", 14), new IdentifierToken("b", 16)));
        Assert.AreEqual(expected, root.Children[0]);

        Assert.AreSame(root, compiler.Parent);
    }

    [Test]
    public void TestInvalidCurrentToken() {
        InitCompiler("(a+b)(a+b) * 2", 5);
        GroupToken groupToken = new GroupToken();
        compiler.Parent.AddChild(groupToken);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreSame(groupToken, root.Children[0]);
        Assert.AreEqual(5, compiler.Pos);
    }
}