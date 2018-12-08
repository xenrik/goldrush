public class CloseToken : RawToken {
    public override string Name { get { return "close"; } }

    public CloseToken() : base() {
    }
    public CloseToken(int position, RawToken parent) : base(position, parent) {
    }
}