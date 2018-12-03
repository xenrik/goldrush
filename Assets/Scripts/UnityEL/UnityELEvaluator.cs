using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityELEvaluator {
    private UnityELExpressionCompiler compiler;

    public UnityELEvaluator() {

    }

    public UnityELExpression<T> Compile<T>(string expression) {
        return compiler.Compile<T>(expression, this);
    }

    public T Evaluate<T>(string expressionString) {
        UnityELExpression<T> expression = Compile<T>(expressionString);
        return expression.Evaluate(this);
    }

}
