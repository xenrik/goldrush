﻿using NUnit.Framework;
using System.Collections.Generic;

public class SubtractionParserTest {
    private RootToken rootToken;
    private RawToken parent;
    private SubtractionParser parser;
    
    [SetUp]
    public void Init() {
        rootToken = new RootToken();
        parent = rootToken;

        parser = new SubtractionParser();
    }

    [Test]
    public void TestValidSubtraction() {
        string expression = "1-2";
        int pos = 1;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new SubtractionToken(1, rootToken), rootToken[0]);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestSubtractionWithSpaces() {
        string expression = "1 - 2";
        int pos = 1;

        Assert.IsTrue(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(1, rootToken.ChildCount);
        Assert.AreEqual(new SubtractionToken(1, rootToken), rootToken[0]);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidSubtraction() {
        string expression = "1+2";
        int pos = 0;

        Assert.IsFalse(parser.Parse(expression.ToCharArray(), ref pos, ref parent));

        Assert.AreSame(rootToken, parent);
        Assert.AreEqual(0, rootToken.ChildCount);
        Assert.AreEqual(0, pos);
    }
}
