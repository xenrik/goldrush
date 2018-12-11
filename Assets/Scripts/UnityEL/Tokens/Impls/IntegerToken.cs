public class IntegerToken : ValueTokenImpl<float> {
    public override string Name { get { return "integer"; } }

    public IntegerToken(float value) : base(value) {
    }
    public IntegerToken(float value, int position) : base(value, position) {
    }
}