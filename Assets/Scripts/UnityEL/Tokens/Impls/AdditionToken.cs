using UnityEngine;
using UnityEditor;

public class AdditionToken : BinaryToken {
    public override string Name { get { return "addition"; } }

    public AdditionToken() {
    }

    public AdditionToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        if (lhsResult.GetType() == typeof(string)) {
            // String addition
            string lhsString = TypeCoercer.CoerceToType<string>(this, lhsResult);
            string rhsString = TypeCoercer.CoerceToType<string>(this, rhsResult);

            return lhsString + rhsString;
        } else {
            // Mathmatic addition
            float lhsFloat = TypeCoercer.CoerceToType<float>(this, lhsResult);
            float rhsFloat = TypeCoercer.CoerceToType<float>(this, rhsResult);

            return lhsFloat + rhsFloat;
        }
    }
}