using System.Collections.Generic;

public class ArgumentToken : RawToken {
    public override string Name { get { return "argument"; } }

    public ArgumentToken() {
    }
    public ArgumentToken(int position, RawToken parent) : base(position, parent) {
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        return null;
    }
}