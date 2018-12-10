using System.Collections.Generic;

public class FunctionToken : RawToken, CloseableToken {
    public override string Name { get { return "function"; } }
    public char CloseTokenSymbol { get { return ')'; } }

    private bool closedParsing = false;
    private bool closedResolve = false;

    private Token identifier;
    private List<Token> arguments;

    public FunctionToken() : base() {
    }
    public FunctionToken(int position, RawToken parent) : base(position, parent) {
    }

    public void ClosedParsing() {
        if (closedParsing) {
            throw new ParserException(this, "Function is already closed");
        }

        this.closedParsing = true;
    }

    public void ClosedResolve() {
        // We must have closed parsing before we can close resolving.
        if (!closedParsing) {
            throw new ParserException(this, "Unclosed Function");
        }
        if (closedResolve) {
            throw new ParserException(this, "Function is already closed");
        }

        this.closedResolve = true;
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        // Resolving must have closed
        if (!closedResolve) {
            throw new ParserException(this, "Unclosed Function");
        }



        return this;
    }
}