using UnityEngine;
using UnityEditor;

public class KeyedAccessParser : SingleCharacterParser {
    public KeyedAccessParser() : base('[') {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        // The current child on the current parent must be an identifier
        bool acceptCurrentChild = false;
        TokenImpl currentChild = compiler.Parent.PeekChild();
        acceptCurrentChild |= currentChild is IdentifierToken;
        acceptCurrentChild |= currentChild is PropertyAccessToken;
        if (!acceptCurrentChild) {
            return false;
        }

        int symbolPos = compiler.Pos;
        if (!base.Parse(compiler)) {
            return false;
        }

        // Pop the current child and it becomes our host name
        KeyedAccessToken keyedAccess = new KeyedAccessToken(symbolPos, compiler.Parent.PopChild());
        compiler.Parent.AddChild(keyedAccess);

        // The keyed access becomes the new parent token
        compiler.ParentTokens.Push(keyedAccess);

        return true;
    }
}