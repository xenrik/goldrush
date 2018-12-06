public class StringToken : ValueToken<string> {
    public StringToken(string value) : base(value) {
    }

    public override string ToString() {
        return "String{" + Value + "}";
    }
}
