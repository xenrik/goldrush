using UnityEngine;
using UnityEditor;

public class EqualsToken : BinaryToken {
    public override string Name { get { return "equals"; } }

    public EqualsToken() {
    }

    public EqualsToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        if (lhsResult is float || lhsResult is int) {
            float lhsFloat = TypeCoercer.CoerceToType<float>(this, lhsResult);
            float rhsFloat = TypeCoercer.CoerceToType<float>(this, rhsResult);

            return lhsFloat == rhsFloat;
        } else if (lhsResult != null) {
            return lhsResult.Equals(rhsResult);
        } else {
            return rhsResult == null;
        }
    }
}