using System;

public class ExponentToken : BinaryToken {
    public override string Name { get { return "exponent"; } }

    public ExponentToken() {
    }

    public ExponentToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        float lhsFloat = TypeCoercer.CoerceToType<float>(this, lhsResult);
        float rhsFloat = TypeCoercer.CoerceToType<float>(this, rhsResult);

        return FastExponent(lhsFloat, rhsFloat);
    }

    /**
     * If possibly avoids using Math.Pow to calculate an exponent (probably won't make much difference,
     * especially as I've not optimised anything else....)
     */
    public static float FastExponent(float value, float exponent) {
        if (exponent == 2) {
            return value * value;
        } else if (exponent == 3) {
            return value * value * value;
        } else {
            return (float)Math.Pow(value, exponent);
        }
    }
}