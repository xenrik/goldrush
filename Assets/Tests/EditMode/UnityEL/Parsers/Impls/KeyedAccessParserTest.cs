﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class KeyedAccessParserTest : BaseParserTest {
    private KeyedAccessParser parser;

    [SetUp]
    public void Init() {
        parser = new KeyedAccessParser();
    }

    [Test]
    public void TestValidStartKeyedAccess() {
        InitCompiler("host['fred']", 4);
        IdentifierToken host = new IdentifierToken("host", 0);
        compiler.Parent.AddChild(host);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(5, compiler.Pos);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new KeyedAccessToken(4, host), root.Children[0]);

        KeyedAccessToken keyedAccessToken = (KeyedAccessToken)root.Children[0];
        Assert.AreSame(keyedAccessToken, compiler.Parent);
    }

    [Test]
    public void TestInvalidStartKeyedAccess() {
        InitCompiler("host(a] * 2", 4);
        IdentifierToken host = new IdentifierToken("host", 0);
        compiler.Parent.AddChild(host);

        Assert.IsFalse(parser.Parse(compiler));

        Assert.AreSame(root, compiler.Parent);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreSame(host, root.Children[0]);
        Assert.AreEqual(4, compiler.Pos);
    }

    [Test]
    public void TestValidStartMemberKeyedAccess() {
        InitCompiler("host.property[123]", 13);
        PropertyAccessToken propertyAccess = new PropertyAccessToken(4,
            new IdentifierToken("host", 0),
            new IdentifierToken("property", 5));
        compiler.Parent.AddChild(propertyAccess);

        Assert.IsTrue(parser.Parse(compiler));

        Assert.AreEqual(14, compiler.Pos);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual(new KeyedAccessToken(13, propertyAccess), root.Children[0]);

        KeyedAccessToken keyedAccessToken = (KeyedAccessToken)root.Children[0];
        Assert.AreSame(keyedAccessToken, compiler.Parent);
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