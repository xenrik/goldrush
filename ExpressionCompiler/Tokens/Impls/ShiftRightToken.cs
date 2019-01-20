public class ShiftRightToken : BinaryToken {
    public override string Name { get { return "shiftRight"; } }

    public ShiftRightToken() {
    }

    public ShiftRightToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        int lhsInt = TypeCoercer.CoerceToType<int>(this, lhsResult);
        int rhsInt = TypeCoercer.CoerceToType<int>(this, rhsResult);

        return lhsInt >> rhsInt;
    }
}