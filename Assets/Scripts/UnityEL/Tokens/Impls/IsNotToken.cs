public class IsNotToken : IsToken {
    public override string Name { get { return "isNot"; } }

    public IsNotToken() {
    }

    public IsNotToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);

        if (Rhs is NullToken) {
            // Special case...
            if (lhsResult != null) {
                return true;
            } else {
                return false;
            }
        }

        // Otherwise, just invert the result of the base class
        return !DoEvaluate(context, lhsResult);
    }
}