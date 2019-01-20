using System.Collections;

public class IsNotEmptyToken : IsEmptyToken {
    public override string Name { get { return "notEmpty"; } }

    public IsNotEmptyToken() {
    }

    public IsNotEmptyToken(int position, TokenImpl rhs) : base(position, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        bool result = (bool)base.Evaluate(context);
        return !result;
    }
}