public class IntegerToken : ValueToken<int> {
    public override string Name { get { return "integer"; } }

    public IntegerToken(int value) : base(value) {
    }

    public IntegerToken(int value, int position, RawToken parent) : base(value, position, parent) {
    }
}