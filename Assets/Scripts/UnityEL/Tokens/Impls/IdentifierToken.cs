public class IdentifierToken : ValueTokenImpl<string> {
    public override string Name { get { return "identifier"; } }

    public IdentifierToken(string value) : base(value) {
    }
    public IdentifierToken(string value, int position) : base(value, position) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        if (context.Properties.ContainsKey(Value)) {
            return context.Properties[Value];
        } else {
            throw new NoSuchPropertyException(this, $"No property named: {Value} was known");
        }
    }
}