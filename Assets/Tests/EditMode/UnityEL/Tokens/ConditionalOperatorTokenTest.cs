using NUnit.Framework;
using System.Collections.Generic;

public class ConditionalOperatorTokenTest {
    private Stack<RawToken> rawTokens;
    private Stack<Token> resolvedTokens;
    private ConditionalOperatorToken rawToken;

    [SetUp]
    public void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();

        rawToken = new ConditionalOperatorToken();
    }

    [Test]
    public void TestResolveValid() {
        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
    }
}
