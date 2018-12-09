public class FunctionToken : RawToken, CloseableToken {
    public override string Name { get { return "function"; } }
    public char CloseTokenSymbol { get { return ')'; } }

    private bool closedParsing = false;
    private bool closedResolve = false;

    public FunctionToken() : base() {
    }
    public FunctionToken(int position, RawToken parent) : base(position, parent) {
    }

    public void ClosedParsing() {
        if (closedParsing) {
            throw new ParserException(this, "Parsing is already closed");
        }

        this.closedParsing = true;
    }

    public void ClosedResolve() {
        if (closedResolve) {
            throw new ParserException(this, "Resolving is already closed");
        }

        this.closedResolve = true;
    }
}