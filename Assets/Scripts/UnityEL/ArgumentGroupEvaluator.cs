using UnityEngine;
using UnityEditor;

/**
 * Used to evaluate argument groups into actual values
 */
public interface ArgumentGroupEvaluator  {
    /**
     * Convert the given group into a value. The target type is unknown
     */
    object Evaluate(UnityELEvaluator context, ArgumentGroupToken group);

    /**
     * COnvert a group to an argument value for the Function of the given name, at the given
     * argument index.
     */
    object EvaluateForArgument(UnityELEvaluator context, string functionname, int argumentIndex, ArgumentGroupToken group);
}