using UnityEngine;
using UnityEditor;

public class ExpressionImpl<T> : UnityELExpression<T> {
    private RootToken root;

    public ExpressionImpl(RootToken root) {
        this.root = root;
    }

    public T Evaluate(UnityELEvaluator context) {
        throw new System.NotImplementedException();
    }
}