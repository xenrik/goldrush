public class BooleanToken : ValueToken<bool> {
    public BooleanToken(bool value) : base(value) {
    }

    public override string ToString() {
        return "Boolean{" + Value + "}";
    }
}