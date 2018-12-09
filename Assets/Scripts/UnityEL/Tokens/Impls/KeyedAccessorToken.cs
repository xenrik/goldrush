public class KeyedAccessorToken : RawToken, CloseableToken {
    public override string Name { get { return "keyedAccessor"; } }
    public char CloseTokenSymbol { get { return ']'; } }

    private bool closedParsing = false;
    private bool closedResolve = false;

    public KeyedAccessorToken() {
    }

    public KeyedAccessorToken(int position, RawToken parent) : base(position, parent) {
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