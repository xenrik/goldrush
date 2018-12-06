public class SubtractionToken : BinaryToken {
    public override string ToString() {
        return "Subtraction{" + Lhs + "," + Rhs + "}";
    }
}