public class DecimalToken : ValueToken<float> {
    public override string Name { get { return "decimal"; } }

    public DecimalToken(float value) : base(value) {
    }
    public DecimalToken(float value, int position, RawToken parent) : base(value, position, parent) {
    }
}