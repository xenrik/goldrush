public class DecimalToken : ValueTokenImpl<float> {
    public override string Name { get { return "decimal"; } }

    public DecimalToken(float value) : base(value) {
    }
    public DecimalToken(float value, int position) : base(value, position) {
    }
}