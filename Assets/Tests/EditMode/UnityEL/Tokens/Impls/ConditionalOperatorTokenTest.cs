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
        IntegerToken lhs = new IntegerToken(1);
        rawTokens.Push(lhs);

        IntegerToken trueValue = new IntegerToken(2);
        IntegerToken falseValue = new IntegerToken(3);

        ConditionalElseToken rhs = new ConditionalElseToken(trueValue, falseValue);
        resolvedTokens.Push(rhs);

        Token result = rawToken.Resolve(rawTokens, resolvedTokens);
        Assert.IsAssignableFrom<ConditionalOperatorToken>(result);

        ConditionalOperatorToken token = (ConditionalOperatorToken)result;
        Assert.AreSame(lhs, token.Test);
        Assert.AreSame(trueValue, token.ResultIfTrue);
        Assert.AreSame(falseValue, token.ResultIfFalse);
    }

    [Test]
    public void TestResolveMissingTest() {
        IntegerToken trueValue = new IntegerToken(2);
        IntegerToken falseValue = new IntegerToken(3);

        ConditionalElseToken rhs = new ConditionalElseToken(trueValue, falseValue);
        resolvedTokens.Push(rhs);

        Assert.Throws<ParserException>(delegate {
            rawToken.Resolve(rawTokens, resolvedTokens);
        });
    }

    [Test]
    public void TestResolveMissingElse() {
        IntegerToken lhs = new IntegerToken(1);
        rawTokens.Push(lhs);

        Assert.Throws<ParserException>(delegate {
            rawToken.Resolve(rawTokens, resolvedTokens);
        });
    }

    [Test]
    public void TestResolveWrongTokenForElse() {
        IntegerToken lhs = new IntegerToken(1);
        rawTokens.Push(lhs);

        IntegerToken rhs = new IntegerToken(2);
        resolvedTokens.Push(rhs);

        Assert.Throws<ParserException>(delegate {
            rawToken.Resolve(rawTokens, resolvedTokens);
        });
    }
}
