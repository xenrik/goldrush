public class NotToken : RawToken {
    public override string Name { get { return "not"; } }

    public NotToken() {
    }

    public NotToken(int position, RawToken parent) : base(position, parent) {
    }
}