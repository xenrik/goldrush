public class DecrementAndReturnToken : UnaryToken {
    public override string Name { get { return "decrementAndReturnToken"; } }

    public DecrementAndReturnToken() {
    }

    public DecrementAndReturnToken(int position, TokenImpl rhs) : base(position, rhs) {
    }

    public override void Validate() {
        base.Validate();

        // Lhs must be Assignable
        if (!(Rhs is AssignableToken)) {
            throw new ParserException($"Unsupported token for right-hand side: {Rhs}");
        }
    }

    public override object Evaluate(UnityELEvaluator context) {
        float oldValue = 0;

        // If the value doesn't already exists (and we can detect that) we start with zero.
        bool readCurrentValue = true;
        if (Rhs is ExistsSupport) {
            readCurrentValue = ((ExistsSupport)Rhs).Exists(context);
        }
        if (readCurrentValue) {
            object current = Rhs.Evaluate(context);
            oldValue = TypeCoercer.CoerceToType<float>(this, current);
        }

        float newValue = oldValue - 1;
        ((AssignableToken)Rhs).Assign(context, newValue);

        return newValue;
    }
}