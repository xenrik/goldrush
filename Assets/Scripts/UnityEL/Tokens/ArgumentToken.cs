using System.Collections.Generic;

public class ArgumentToken : BaseToken {
    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        return null;
    }

    public override string ToString() {
        return "Argument{-TBD-}";
    }
}