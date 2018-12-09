public class SubtractionToken : BinaryToken {
    public override string Name { get { return "subtraction"; } }

    public SubtractionToken() {
    }
    public SubtractionToken(int position, RawToken parent) : base(position, parent) {
    }
}