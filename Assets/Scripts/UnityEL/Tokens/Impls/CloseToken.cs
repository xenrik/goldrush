using System.Collections.Generic;

public class CloseToken : RawToken {
    public override string Name { get { return "close"; } }

    public CloseToken() : base() {
    }
    public CloseToken(int position, RawToken parent) : base(position, parent) {
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        // Just call close resolve on the parent (which should be closeable for us to get here!)
        ((CloseableToken)Parent).ClosedResolve();

        return this;
    }
}