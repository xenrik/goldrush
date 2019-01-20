public class OrToken : BinaryToken {
    public override string Name { get { return "or"; } }

    public OrToken() {
    }

    public OrToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        bool lhsBool = TypeCoercer.CoerceToType<bool>(this, lhsResult);
        bool rhsBool = TypeCoercer.CoerceToType<bool>(this, rhsResult);

        return lhsBool || rhsBool;
    }
}