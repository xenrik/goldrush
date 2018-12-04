public class DecimalToken : ValueToken<float> {
    public DecimalToken(float value) : base(value) {
    }

    public override string ToString() {
        return "Decimal{" + Value + "}";
    }
}