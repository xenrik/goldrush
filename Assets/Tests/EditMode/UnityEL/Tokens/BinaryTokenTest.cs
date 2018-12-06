using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public abstract class BinaryTokenTest<T> where T : BinaryToken, new() {
    protected Stack<RawToken> rawTokens;
    protected Stack<Token> resolvedTokens;
    protected RawToken rawToken;

    [SetUp]
    public virtual void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();

        rawToken = new T();
    }

    [Test]
    public void TestResolveValid() {
        IntegerToken lhs = new IntegerToken(1);
        rawTokens.Push(lhs);

        IntegerToken rhs = new IntegerToken(2);
        resolvedTokens.Push(rhs);

        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
        Assert.IsAssignableFrom<T>(result);

        T token = (T)result;
        Assert.AreSame(lhs, token.Lhs);
        Assert.AreSame(rhs, token.Rhs);
    }

    [Test]
    public void TestResolveMissingLhs() {
        IntegerToken rhs = new IntegerToken(2);
        resolvedTokens.Push(rhs);

        Assert.Throws<ParserException>(delegate {
            rawToken.Resolve(rawTokens, resolvedTokens);
        });
    }

    [Test]
    public void TestResolveMissingRhs() {
        IntegerToken lhs = new IntegerToken(1);
        rawTokens.Push(lhs);

        Assert.Throws<ParserException>(delegate {
            rawToken.Resolve(rawTokens, resolvedTokens);
        });
    }
}