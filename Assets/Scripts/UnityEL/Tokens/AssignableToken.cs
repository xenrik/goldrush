using UnityEngine;
using UnityEditor;

/**
 * Simple interface that tokens can support assignment
 */
public interface AssignableToken {
    /** Assign a value to the token */
    void Assign(UnityELEvaluator context, object value);
}