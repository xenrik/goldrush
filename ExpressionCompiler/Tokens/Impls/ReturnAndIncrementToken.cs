public class ReturnAndIncrementToken : LeftHandUnaryToken {
    public override string Name { get { return "returnAndIncrementToken"; } }

    public ReturnAndIncrementToken() {
    }

    public ReturnAndIncrementToken(int position, TokenImpl lhs) : base(position, lhs) {
    }

    public override void Validate() {
        base.Validate();

        // Lhs must be Assignable
        if (!(Lhs is AssignableToken)) {
            throw new ParserException($"Unsupported token for left-hand side: {Lhs}");
        }
    }

    public override object Evaluate(UnityELEvaluator context) {
        float oldValue = 0;

        // If the value doesn't already exists (and we can detect that) we start with zero.
        bool readCurrentValue = true;
        if (Lhs is ExistsSupport) {
            readCurrentValue = ((ExistsSupport)Lhs).Exists(context);
        }
        if (readCurrentValue) {
            object current = Lhs.Evaluate(context);
            oldValue = TypeCoercer.CoerceToType<float>(this, current);
        }

        float newValue = oldValue + 1;
        ((AssignableToken)Lhs).Assign(context, newValue);

        return oldValue;
    }
}