public class IntegerToken : ValueToken<int> {
    public IntegerToken(int value) : base(value) {
    }

    public override string ToString() {
        return "Integer{" + Value + "}";
    }
}