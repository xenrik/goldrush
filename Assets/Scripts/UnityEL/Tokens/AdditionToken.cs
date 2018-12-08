public class AdditionToken : BinaryToken {
    public override string Name { get { return "addition"; } }

    public AdditionToken(int position, RawToken parent) : base(position, parent) {
    }
}