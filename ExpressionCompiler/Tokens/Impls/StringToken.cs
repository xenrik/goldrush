public class StringToken : ValueTokenImpl<string> {
    public override string Name { get { return "string"; } }

    public StringToken(string value) : base(value) {
    }
    public StringToken(string value, int position) : base(value, position) {
    }
}