using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public abstract class BinaryParserTest<P,T> 
        where P : TokenParser, new() 
        where T : BinaryToken {
    public abstract string ParserSymbol { get; }

    private RootToken rootToken;
    private RawToken parent;

    private TokenParser parser;

    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        this.parser = new P();
    }

    [Test]
    public void TestValidExpression() {
        string expression = $"1{ParserSymbol}2";
        int pos = 1;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, rootToken.ChildCount);

        RawToken expected = (RawToken)System.Activator.CreateInstance(typeof(T), 1, rootToken);
        Assert.AreEqual(expected, rootToken[0]);

        Assert.AreEqual(1 + ParserSymbol.Length, pos);
    }

    [Test]
    public void TestAdditionWithSpaces() {
        string expression = $"1 {ParserSymbol} 2";
        int pos = 1;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);

        Assert.AreEqual(1, rootToken.ChildCount);

        RawToken expected = (RawToken)System.Activator.CreateInstance(typeof(T), 1, rootToken);
        Assert.AreEqual(expected, rootToken[0]);

        Assert.AreEqual(2 + ParserSymbol.Length, pos);
    }

    [Test]
    public void TestInvalidAddition() {
        string invalidString = new string('¬', ParserSymbol.Length);
        string expression = $"1{invalidString}2";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }
}