public class IdentifierToken : ValueTokenImpl<string> {
    public override string Name { get { return "identifier"; } }

    public IdentifierToken(string value) : base(value) {
    }
    public IdentifierToken(string value, int position) : base(value, position) {
    }
}