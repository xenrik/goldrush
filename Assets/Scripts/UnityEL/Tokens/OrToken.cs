public class OrToken : BinaryToken {
    public override string Name { get { return "or"; } }

    public OrToken(int position, RawToken parent) : base(position, parent) {
    }
}