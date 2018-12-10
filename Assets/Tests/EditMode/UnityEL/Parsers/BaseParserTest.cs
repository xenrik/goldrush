using UnityEngine;
using UnityEditor;

public abstract class BaseParserTest {
    public ExpressionCompiler compiler;
    public RootToken root;

    public void InitCompiler(string expression, int pos) {
        root = new RootToken();

        compiler = new ExpressionCompiler(expression);
        compiler.ParentTokens.Push(root);
        compiler.Pos = pos;
    }
}