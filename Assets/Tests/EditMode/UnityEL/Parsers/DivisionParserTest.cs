﻿using NUnit.Framework;
using System.Collections.Generic;

public class DivisionParserTest {
    private DivisionParser parser;
    
    [SetUp]
    public void Init() {
        parser = new DivisionParser();
    }

    [Test]
    public void TestValidDivision() {
        string expression = "1/2";
        int pos = 1;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<DivisionToken>(result);
        Assert.AreEqual(2, pos);
    }

    [Test]
    public void TestDivisionWithSpaces() {
        string expression = "1 / 2";
        int pos = 1;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<DivisionToken>(result);
        Assert.AreEqual(3, pos);
    }

    [Test]
    public void TestInvalidDivision() {
        string expression = "1*2";
        int pos = 0;
        Token result = parser.Parse(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(0, pos);
    }
}
