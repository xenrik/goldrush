using System.Collections.Generic;

public interface RawToken : Token {
    /**
     * Resolve this raw token into a resolved token
     */
    Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens);
}