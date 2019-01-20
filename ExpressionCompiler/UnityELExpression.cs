public interface UnityELExpression<T> {
    T Evaluate(UnityELEvaluator context);
}
