public class ConditionalOperatorToken : RawToken {
    public override string Name { get { return "conditionalOperator"; } }

    public ConditionalOperatorToken() {
    }

    public ConditionalOperatorToken(int position, RawToken parent) : base(position, parent) {
    }
}