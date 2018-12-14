﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityELEvaluator {
    public Dictionary<string, object> Properties { get; private set; }
    

    public UnityELEvaluator() {
        Properties = new Dictionary<string, object>();
    }

    public UnityELExpression<T> Compile<T>(string expression) {
        ExpressionCompiler compiler = new ExpressionCompiler(expression);
        return compiler.Compile<T>();
    }

    public T Evaluate<T>(string expressionString) {
        UnityELExpression<T> expression = Compile<T>(expressionString);
        return expression.Evaluate(this);
    }
}
