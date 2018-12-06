using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class BinaryToken : BaseToken {
    public Token Lhs { get; private set; }
    public Token Rhs { get; private set; }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        // Take the next raw token as the LHS, and the current resolvedToken as the RHS
        if (rawTokens.Count == 0 || resolvedTokens.Count == 0) {
            throw new ParserException("Missing operand");
        }

        Lhs = rawTokens.Pop().Resolve(rawTokens, resolvedTokens);
        Rhs = resolvedTokens.Pop();

        return this;
    }
}