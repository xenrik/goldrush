public class LessThanToken : BinaryToken {
    public override string Name { get { return "lessThan"; } }

    public LessThanToken() {
    }

    public LessThanToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        float lhsFloat = TypeCoercer.CoerceToType<float>(this, lhsResult);
        float rhsFloat = TypeCoercer.CoerceToType<float>(this, rhsResult);

        return lhsFloat < rhsFloat;
    }
}