using NUnit.Framework;
using System.Collections.Generic;

public class CloseTokenTest {
    private Stack<RawToken> rawTokens;
    private Stack<Token> resolvedTokens;

    [SetUp]
    public void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();
    }

    [Test]
    public void TestResolveCloseGroupValid() {
        GroupToken group = new GroupToken();
        CloseToken close = new CloseToken(0, group);

        Token result = close.Resolve(rawTokens, resolvedTokens);
        Assert.AreSame(close, result);
    }

    [Test]
    public void TestResolveCloseKeyedAccessValid() {
        KeyedAccessorToken keyedAccessor = new KeyedAccessorToken();
        CloseToken close = new CloseToken(0, keyedAccessor);

        Token result = close.Resolve(rawTokens, resolvedTokens);
        Assert.AreSame(close, result);
    }

    [Test]
    public void TestResolveCloseFunctionValid() {
        FunctionToken function = new FunctionToken();
        CloseToken close = new CloseToken(0, function);

        Token result = close.Resolve(rawTokens, resolvedTokens);
        Assert.AreSame(close, result);
    }
}
