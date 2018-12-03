using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnityELExpression<T> {
    T Evaluate(UnityELEvaluator context);
}
