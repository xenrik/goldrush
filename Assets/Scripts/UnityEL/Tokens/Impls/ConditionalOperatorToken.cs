using System.Collections.Generic;

public class ConditionalOperatorToken : RawToken {
    public override string Name { get { return "conditionalOperator"; } }

    public Token Test { get; private set; }
    public Token ResultIfTrue { get; private set; }
    public Token ResultIfFalse { get; private set; }

    public ConditionalOperatorToken() {
    }

    public ConditionalOperatorToken(int position, RawToken parent) : base(position, parent) {
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        // Take the next raw token as the Test, and the current resolvedToken should be a ConditionalElseToken
        if (rawTokens.Count == 0 || resolvedTokens.Count == 0) {
            throw new ParserException(this, "Missing operand");
        }

        Test = rawTokens.Pop().Resolve(rawTokens, resolvedTokens);
        Token rhs = resolvedTokens.Pop();
        if (!(rhs is ConditionalElseToken)) {
            throw new ParserException(this, "Expected else part of the conditional operator, but found: " + rhs);
        }

        ConditionalElseToken conditionalElse = (ConditionalElseToken)rhs;
        ResultIfTrue =
        return this;
    }
}