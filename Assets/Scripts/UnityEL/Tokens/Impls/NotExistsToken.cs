using System.Collections;

public class NotExistsToken : UnaryToken {
    public override string Name { get { return "notExists"; } }

    public NotExistsToken() {
    }

    public NotExistsToken(int position, TokenImpl rhs) : base(position, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        if (Rhs is ExistsSupport) {
            return !((ExistsSupport)Rhs).Exists(context);
        } else {
            throw new ParserException($"Unsupport token type for NotExists: {Rhs.Name}");
        }
    }
}