using UnityEngine;
using UnityEditor;

public class FunctionParser : SingleCharacterParser {
    public FunctionParser() : base('(') {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        // The current child on the current parent must be an identifier
        bool acceptCurrentChild = false;
        acceptCurrentChild |= compiler.Parent.PeekChild() is IdentifierToken;
        acceptCurrentChild |= compiler.Parent.PeekChild() is PropertyAccessToken;
        if (!acceptCurrentChild) {
            return false;
        }

        int symbolPos = compiler.Pos;
        if (!base.Parse(compiler)) {
            return false;
        }

        // The function becomes the new parent token
        FunctionToken function = new FunctionToken(symbolPos, compiler.Parent.PopChild());
        compiler.Parent.AddChild(function);
        compiler.ParentTokens.Push(function);

        return true;
    }
}