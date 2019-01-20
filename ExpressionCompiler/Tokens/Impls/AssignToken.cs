public class AssignToken : BinaryToken {
    public override string Name { get { return "assign"; } }

    public AssignToken() {
    }

    public AssignToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override void Validate() {
        base.Validate();

        // Lhs must be assignable
        if (!(Lhs is AssignableToken)) {
            throw new ParserException($"Unsupported token for left-hand side: {Lhs}");
        }
    }

    public override object Evaluate(UnityELEvaluator context) {
        object value = Rhs.Evaluate(context);
        ((AssignableToken)Lhs).Assign(context, value);

        return value;
    }
}