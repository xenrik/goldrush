using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;

public abstract class UnaryTokenTest<T> where T : UnaryToken {
    protected Stack<RawToken> rawTokens;
    protected Stack<Token> resolvedTokens;
    protected RawToken rawToken;

    [SetUp]
    public virtual void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();

        rawToken = System.Activator.CreateInstance<T>();
    }

    [Test]
    public void TestResolveValid() {
        IntegerToken operand = new IntegerToken(2);
        resolvedTokens.Push(operand);

        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
        Assert.IsAssignableFrom<T>(result);

        T token = (T)result;
        Assert.AreSame(operand, token.Operand);
    }

    [Test]
    public void TestResolveMissingOperand() {
        Assert.Throws<ParserException>(delegate {
            rawToken.Resolve(rawTokens, resolvedTokens);
        });
    }
}