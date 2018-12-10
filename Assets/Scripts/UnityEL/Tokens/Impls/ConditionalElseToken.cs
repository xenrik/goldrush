using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ConditionalElseToken : RawToken {
    public override string Name { get { return "conditionalElse"; } }

    public Token ResultIfTrue { get; private set; }
    public Token ResultIfFalse { get; private set; }

    public ConditionalElseToken() {
    }

    public ConditionalElseToken(int position, RawToken parent) : base(position, parent) {
    }

    /**
     * Constructor that accepts the left and right-hand sides of the conditional 'else' operator. Primarily
     * intended for unit tests
     */
    public ConditionalElseToken(Token resultIfTrue, Token resultIfFalse) : base() {
        ResultIfTrue = resultIfTrue;
        ResultIfFalse = resultIfFalse;
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        // Take the next raw token as the ResultIfTrue, and the current resolvedToken as the ResultIfFalse
        if (rawTokens.Count == 0 || resolvedTokens.Count == 0) {
            throw new ParserException(this, "Missing operand");
        }

        ResultIfTrue = rawTokens.Pop().Resolve(rawTokens, resolvedTokens);
        ResultIfFalse = resolvedTokens.Pop();

        return this;
    }
}