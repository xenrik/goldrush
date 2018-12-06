public class AndToken : BinaryToken {
    public override string ToString() {
        return "And{" + Lhs + "," + Rhs + "}";
    }
}