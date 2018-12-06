public class OrToken : BinaryToken {
    public override string ToString() {
        return "Or{" + Lhs + "," + Rhs + "}";
    }
}