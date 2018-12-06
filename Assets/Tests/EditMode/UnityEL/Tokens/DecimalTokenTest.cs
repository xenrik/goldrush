using NUnit.Framework;
using System.Collections.Generic;

public class DecimalTokenTest {
    private Stack<RawToken> rawTokens;
    private Stack<Token> resolvedTokens;
    private DecimalToken rawToken;

    [SetUp]
    public void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();

        rawToken = new DecimalToken(123.45f);
    }

    [Test]
    public void TestResolveValid() {
        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
    }
}
