using UnityEngine;
using UnityEditor;

public class ComplementToken : UnaryToken {
    public override string Name { get { return "complement"; } }

    public ComplementToken() {
    }

    public ComplementToken(int position, TokenImpl rhs) : base(position, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object rhsResult = Rhs.Evaluate(context);
        int rhsInt = TypeCoercer.CoerceToType<int>(this, rhsResult);

        return ~rhsInt;
    }
}