public class MultiplicationToken : BinaryToken {
    public override string ToString() {
        return "Multiplication{" + Lhs + "," + Rhs + "}";
    }
}