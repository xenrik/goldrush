public class ExponentToken : BinaryToken {
    public override string ToString() {
        return "Exponent{" + Lhs + "," + Rhs + "}";
    }
}