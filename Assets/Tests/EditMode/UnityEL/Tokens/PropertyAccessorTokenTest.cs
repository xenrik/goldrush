using NUnit.Framework;
using System.Collections.Generic;

public class PropertyAccessorTokenTest {
    private Stack<RawToken> rawTokens;
    private Stack<Token> resolvedTokens;
    private PropertyAccessorToken rawToken;

    [SetUp]
    public void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();

        rawToken = new PropertyAccessorToken();
    }

    [Test]
    public void TestResolveValid() {
        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
    }
}
