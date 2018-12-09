public class MultiplicationToken : BinaryToken {
    public override string Name { get { return "multiplication"; } }

    public MultiplicationToken() {
    }
    public MultiplicationToken(int position, RawToken parent) : base(position, parent) {
    }
}