public class BitwiseOrToken : BinaryToken {
    public override string Name { get { return "bitwiseOr"; } }

    public BitwiseOrToken(int position, RawToken parent) : base(position, parent) {
    }
}