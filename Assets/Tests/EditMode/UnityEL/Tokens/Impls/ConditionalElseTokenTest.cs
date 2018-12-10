using NUnit.Framework;
using System.Collections.Generic;

public class ConditionalElseTokenTest {
    private Stack<RawToken> rawTokens;
    private Stack<Token> resolvedTokens;
    private ConditionalElseToken rawToken;

    [SetUp]
    public void Init() {
        rawTokens = new Stack<RawToken>();
        resolvedTokens = new Stack<Token>();

        rawToken = new ConditionalElseToken();
    }

    [Test]
    public void TestResolveValid() {
        IntegerToken lhs = new IntegerToken(1);
        rawTokens.Push(lhs);

        IntegerToken rhs = new IntegerToken(2);
        resolvedTokens.Push(rhs);

        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
        Assert.IsAssignableFrom<ConditionalElseToken>(result);

        ConditionalElseToken token = (ConditionalElseToken)result;
        Assert.AreSame(lhs, token.ResultIfTrue);
        Assert.AreSame(rhs, token.ResultIfFalse);
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
