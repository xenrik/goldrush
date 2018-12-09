public class IdentifierToken : ValueToken<string> {
    public override string Name { get { return "identifier"; } }

    public IdentifierToken(string value) : base(value) {
    }
    public IdentifierToken(string value, int position, RawToken parent) : base(value, position, parent) {
    }
}
