public class ExponentToken : BinaryToken {
    public override string Name { get { return "exponent"; } }

    public ExponentToken(int position, RawToken parent) : base(position, parent) {
    }
}