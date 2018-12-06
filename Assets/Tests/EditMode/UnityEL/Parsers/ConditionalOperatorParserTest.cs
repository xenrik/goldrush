﻿using NUnit.Framework;
using System.Collections.Generic;

public class ConditionalOperatorParserTest {
    private ConditionalOperatorParser parser;
    
    [SetUp]
    public void Init() {
        parser = new ConditionalOperatorParser();
    }

    [Test]
    public void TestValidConditionalOperator() {
        string expression = "(true)?1:2";
        int pos = 6;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ConditionalOperatorToken>(result);
        Assert.AreEqual(7, pos);
    }

    [Test]
    public void TestConditionalOperatorWithSpaces() {
        string expression = "(true) ? 1 : 2";
        int pos = 6;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsAssignableFrom<ConditionalOperatorToken>(result);
        Assert.AreEqual(8, pos);
    }

    [Test]
    public void TestInvalidConditionalOperator() {
        string expression = "(true) ! 2 ? 1";
        int pos = 6;
        Token result = parser.Consume(expression.ToCharArray(), ref pos);

        Assert.IsNull(result);
        Assert.AreEqual(6, pos);
    }
}
