using UnityEngine;
using UnityEditor;

public class ArgumentGroupToken : TokenImpl, CloseableToken {
    public override string Name { get { return "argumentGroup"; } }
    public bool IsClosed { get; private set; }

    public ArgumentGroupToken() {
    }

    public ArgumentGroupToken(int position) : base(position) {
    }

    public void Close() {
        if (IsClosed) {
            throw new ParserException(this, "Argument group has already been closed");
        }

        IsClosed = true;
    }

    public override void Validate() {
        if (!IsClosed) {
            throw new ParserException(this, "Has not been closed");
        }
    }

    public override object Evaluate(UnityELEvaluator context) {
        if (context.ArgumentGroupEvaluator == null) {
            throw new ParserException(this, "Cannot convert argument group to a value as no ArgumentGroupConverter has been configured on the evaluator");
        }

        return context.ArgumentGroupEvaluator.Evaluate(context, this);
    }

    /**
     * Specialised version of evaluate which is used when the argument group is evaluated in the scope of a function
     */
    public object EvaluateForArgument(UnityELEvaluator context, string functionName, int argumentIndex) {
        if (context.ArgumentGroupEvaluator == null) {
            throw new ParserException(this, "Cannot convert argument group to an argument as no ArgumentGroupConverter has been configured on the evaluator");
        }

        return context.ArgumentGroupEvaluator.EvaluateForArgument(context, functionName, argumentIndex, this);
    }
}