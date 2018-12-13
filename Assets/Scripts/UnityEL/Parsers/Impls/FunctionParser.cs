using UnityEngine;
using UnityEditor;

public class FunctionParser : SingleCharacterParser {
    public FunctionParser() : base('(') {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        // The current child on the current parent must be an identifier
        if (!(compiler.Parent.PeekChild() is IdentifierToken)) {
            return false;
        }

        int symbolPos = compiler.Pos;
        if (!base.Parse(compiler)) {
            return false;
        }

        // The function becomes the new parent token
        IdentifierToken identifier = (IdentifierToken)compiler.Parent.PopChild();
        FunctionToken function = new FunctionToken(symbolPos, identifier);
        compiler.Parent.AddChild(function);
        compiler.ParentTokens.Push(function);

        return true;
    }
}