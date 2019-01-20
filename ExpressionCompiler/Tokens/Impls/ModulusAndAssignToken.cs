public class ModulusAndAssignToken : BinaryToken {
    public override string Name { get { return "modulusAndAssign"; } }

    public ModulusAndAssignToken() {
    }

    public ModulusAndAssignToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override void Validate() {
        base.Validate();

        // Lhs must be Assignable
        if (!(Lhs is AssignableToken)) {
            throw new ParserException($"Unsupported token for left-hand side: {Lhs}");
        }
    }

    public override object Evaluate(UnityELEvaluator context) {
        object amount = Rhs.Evaluate(context);
        float floatAmount = TypeCoercer.CoerceToType<float>(this, amount);

        float floatCurrent = 0;

        // If the value doesn't already exists (and we can detect that) we start with zero.
        bool readCurrentValue = true;
        if (Lhs is ExistsSupport) {
            readCurrentValue = ((ExistsSupport)Lhs).Exists(context);
        }
        if (readCurrentValue) {
            object current = Lhs.Evaluate(context);
            floatCurrent = TypeCoercer.CoerceToType<float>(this, current);
        }

        float result = floatCurrent % floatAmount;
        ((AssignableToken)Lhs).Assign(context, result);

        return result;
    }
}