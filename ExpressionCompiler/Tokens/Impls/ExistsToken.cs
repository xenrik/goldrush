using System.Collections;

public class ExistsToken : UnaryToken {
    public override string Name { get { return "exists"; } }

    public ExistsToken() {
    }

    public ExistsToken(int position, TokenImpl rhs) : base(position, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        if (Rhs is ExistsSupport) {
            return ((ExistsSupport)Rhs).Exists(context);
        } else {
            throw new ParserException($"Unsupport token type for exists: {Rhs.Name}");
        }
    }
}