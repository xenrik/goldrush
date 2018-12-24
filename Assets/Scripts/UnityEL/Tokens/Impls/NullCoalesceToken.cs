using UnityEngine;
using UnityEditor;

public class NullCoalesceToken : BinaryToken {
    public override string Name { get { return "nullCoalesce"; } }

    public NullCoalesceToken() {
    }

    public NullCoalesceToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        return lhsResult ?? rhsResult;
    }
}