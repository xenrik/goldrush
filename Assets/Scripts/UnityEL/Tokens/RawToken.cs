using System.Collections.Generic;

public interface RawToken : Token {
    /** The position this token started at in the expression */
    int Position { get; }

    /**
     * The parent of this token
     */
    RawToken Parent { get; set; }

    /**
     * Add a child token to this token
     */
    void AddToken(RawToken child);

    /**
     * Resolve this raw token into a resolved token
     */
    Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens);
}