﻿using NUnit.Framework;
using System.Collections.Generic;

public class XorParserTest {
    private XorParser parser;
    private Stack<Token> tokenStack;
    
    [SetUp]
    public void Init() {
        parser = new XorParser();
        tokenStack = new Stack<Token>();
    }

    [Test]
    public void TestValidXor() {
        string expression = "0b1010^0b1001";
        int pos = 6;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<XorToken>(result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestXorWithSpaces() {
        string expression = "0b1010 ^ 0b1001";
        int pos = 6;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<XorToken>(result);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestInvalidXor() {
        string expression = "0b1010#0b1001";
        int pos = 6;
        Token result = parser.Consume(tokenStack, expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(6, pos);
    }
}
