public class NotToken : UnaryToken {
    public override string Name { get { return "not"; } }

    public NotToken() {
    }

    public NotToken(int position, RawToken parent) : base(position, parent) {
    }
}