public class XorToken : BinaryToken {
    public override string ToString() {
        return "Xor{" + Lhs + "," + Rhs + "}";
    }
}