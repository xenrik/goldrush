public class ModulusToken : BinaryToken {
    public override string Name { get { return "modulus"; } }

    public ModulusToken() {
    }
    public ModulusToken(int position, RawToken parent) : base(position, parent) {
    }
}