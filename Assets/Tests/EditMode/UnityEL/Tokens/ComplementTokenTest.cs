using NUnit.Framework;
using System.Collections.Generic;

public class ComplementTokenTest {
    private Stack<RawToken> rawTokens;
    private Stack<Token> resolvedTokens;
    private ComplementToken rawToken;

    [SetUp]
    public void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();

        rawToken = new ComplementToken();
    }

    [Test]
    public void TestResolveValid() {
        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
    }
}
