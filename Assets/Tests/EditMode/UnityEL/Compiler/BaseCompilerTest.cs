using UnityEngine;
using UnityEditor;

public abstract class BaseCompilerTest {
    public ExpressionCompiler compiler;
    public UnityELEvaluator evaluator;

    public void InitCompiler(string expression) {
        compiler = new ExpressionCompiler(expression);
        evaluator = new UnityELEvaluator();
    }
}