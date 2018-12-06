public class NullCoalesceToken : BinaryToken {
    public override string ToString() {
        return "NullCoalesce{" + Lhs + "," + Rhs + "}";
    }
}