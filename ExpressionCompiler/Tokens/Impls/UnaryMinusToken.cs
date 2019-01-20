public class UnaryMinusToken : UnaryToken {
    public override string Name { get { return "unaryMinus"; } }

    public UnaryMinusToken() {
    }

    public UnaryMinusToken(int position, TokenImpl rhs) : base(position, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object rhsResult = Rhs.Evaluate(context);
        float rhsFloat = TypeCoercer.CoerceToType<float>(this, rhsResult);

        return -rhsFloat;
    }
}