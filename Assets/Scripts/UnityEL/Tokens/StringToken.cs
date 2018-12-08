public class StringToken : ValueToken<string> {
    public override string Name { get { return "string"; } }

    public StringToken(string value) : base(value) {
    }
    public StringToken(string value, int position, RawToken parent) : base(value, position, parent) {
    }
}
