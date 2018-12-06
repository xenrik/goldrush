using NUnit.Framework;
using System.Collections.Generic;

public class CloseTokenTest {
    private Stack<RawToken> rawTokens;
    private Stack<Token> resolvedTokens;
    private CloseToken rawToken;

    [SetUp]
    public void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();

        rawToken = new CloseToken();
    }

    [Test]
    public void TestResolveValid() {
        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
    }
}
