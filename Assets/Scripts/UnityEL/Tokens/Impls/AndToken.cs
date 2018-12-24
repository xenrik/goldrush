using UnityEngine;
using UnityEditor;

public class AndToken : BinaryToken {
    public override string Name { get { return "and"; } }

    public AndToken() {
    }

    public AndToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        bool lhsBool = TypeCoercer.CoerceToType<bool>(this, lhsResult);
        bool rhsBool = TypeCoercer.CoerceToType<bool>(this, rhsResult);

        return lhsBool && rhsBool;
    }
}