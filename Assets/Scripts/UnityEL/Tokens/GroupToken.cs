public class GroupToken : RawToken, CloseableToken {
    public override string Name { get { return "group"; } }

    private bool isClosed = false;

    public GroupToken() {
    }

    public GroupToken(int position, RawToken parent) : base(position, parent) {
    }

    public void Close() {
        if (isClosed) {
            throw new ParserException(this, "Already Closed");
        }

        this.isClosed = true;
    }
}