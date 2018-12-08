public class FunctionToken : RawToken {
    public override string Name { get { return "function"; } }

    public FunctionToken() : base() {
    }
    public FunctionToken(int position, RawToken parent) : base(position, parent) {
    }
}