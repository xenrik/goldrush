public class DivisionToken : BinaryToken {
    public override string Name { get { return "division"; } }

    public DivisionToken() {
    }
    public DivisionToken(int position, RawToken parent) : base(position, parent) {
    }
}