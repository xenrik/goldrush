using UnityEngine;
using UnityEditor;

public class ExpressionImpl<T> : UnityELExpression<T> {
    private RootToken root;

    public ExpressionImpl(RootToken root) {
        this.root = root;
    }

    public T Evaluate(UnityELEvaluator context) {
        object result = root.Evaluate(context);
        return TypeCoercer.CoerceToType<T>(root, result);
    }
}