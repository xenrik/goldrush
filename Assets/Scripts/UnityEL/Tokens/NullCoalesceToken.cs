public class NullCoalesceToken : BinaryToken {
    public override string Name { get { return "nullCoalesce"; } }

    public NullCoalesceToken(int position, RawToken parent) : base(position, parent) {
    }
}