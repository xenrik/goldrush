public class NotToken : UnaryToken {
    public override string Name { get { return "not"; } }

    public NotToken() {
    }

    public NotToken(int position, TokenImpl rhs) : base(position, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object rhsResult = Rhs.Evaluate(context);
        bool rhsBool = TypeCoercer.CoerceToType<bool>(this, rhsResult);

        return !rhsBool;
    }
}