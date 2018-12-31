using UnityEngine;
using UnityEditor;

public class GreaterThanOrEqualsToken : BinaryToken {
    public override string Name { get { return "greaterThanOrEquals"; } }

    public GreaterThanOrEqualsToken() {
    }

    public GreaterThanOrEqualsToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        float lhsFloat = TypeCoercer.CoerceToType<float>(this, lhsResult);
        float rhsFloat = TypeCoercer.CoerceToType<float>(this, rhsResult);

        return lhsFloat >= rhsFloat;
    }
}