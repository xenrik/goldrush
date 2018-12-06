using System.Collections.Generic;

public class AdditionToken : BinaryToken {
    public override string ToString() {
        return "Addition{" + Lhs + "," + Rhs + "}";
    }
}