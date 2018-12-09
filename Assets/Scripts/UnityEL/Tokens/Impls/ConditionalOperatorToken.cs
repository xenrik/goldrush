public class ConditionalOperatorToken : RawToken {
    public override string Name { get { return "conditionalOperator"; } }

    public RawToken Test { get; private set; }
    public RawToken ResultIfTrue { get; private set; }
    public RawToken ResultIfFalse { get; private set; }

    public ConditionalOperatorToken() {
    }

    public ConditionalOperatorToken(int position, RawToken parent) : base(position, parent) {
    }
}