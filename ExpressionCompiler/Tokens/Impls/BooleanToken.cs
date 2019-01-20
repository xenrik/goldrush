public class BooleanToken : ValueTokenImpl<bool> {
    public override string Name { get { return "boolean"; } }

    public BooleanToken(bool value) : base(value) {
    }

    public BooleanToken(bool value, int position) : base(value, position) {
    }
}