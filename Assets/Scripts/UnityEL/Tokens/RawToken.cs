using System.Collections.Generic;

public interface RawToken : Token {
    /**
     * Resolve this raw toekn into a resolved token
     */
    Token Resolve(Stack<RawToken> rawTokens);
}