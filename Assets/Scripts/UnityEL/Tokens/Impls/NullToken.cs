public class NullToken : ValueTokenImpl<object> {
    public override string Name { get { return "nullToken"; } }

    public NullToken() : base(null) {
    }

    public NullToken(int position) : base(null, position) {
    }
}