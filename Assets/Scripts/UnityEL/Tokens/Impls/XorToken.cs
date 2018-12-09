public class XorToken : BinaryToken {
    public override string Name { get { return "xor"; } }

    public XorToken() {
    }
    public XorToken(int position, RawToken parent) : base(position, parent) {
    }
}