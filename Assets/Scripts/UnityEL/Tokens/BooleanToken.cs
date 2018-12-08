public class BooleanToken : ValueToken<bool> {
    public override string Name { get { return "boolean"; } }

    public BooleanToken(bool value) : base(value) {
    }

    public BooleanToken(bool value, int position, RawToken parent) : base(value, position, parent) {
    }
}