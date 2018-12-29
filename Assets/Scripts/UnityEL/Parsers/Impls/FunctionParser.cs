using UnityEngine;
using UnityEditor;

public class FunctionParser : SingleCharacterParser {
    public FunctionParser() : base('(') {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        // The current child on the current parent must be an identifier
        bool acceptCurrentChild = false;
        TokenImpl currentChild = compiler.Parent.PeekChild();
        acceptCurrentChild |= currentChild is IdentifierToken;
        acceptCurrentChild |= currentChild is PropertyAccessToken;
        acceptCurrentChild |= currentChild is BinaryToken &&
            (((BinaryToken)currentChild).Rhs is IdentifierToken ||
             ((BinaryToken)currentChild).Rhs is PropertyAccessToken);
        if (!acceptCurrentChild) {
            return false;
        }

        int symbolPos = compiler.Pos;
        if (!base.Parse(compiler)) {
            return false;
        }

        FunctionToken function;
        if (currentChild is IdentifierToken ||
            currentChild is PropertyAccessToken) {
            // Pop the current child and it becomes our function name
            function = new FunctionToken(symbolPos, compiler.Parent.PopChild());
            compiler.Parent.AddChild(function);

            // The function becomes the new parent token
            compiler.ParentTokens.Push(function);
        } else {
            // Must be binary to get here, the RHS of the binary token becomes
            // our function name and we become the RHS of the binary token.
            BinaryToken binaryToken = (BinaryToken)currentChild;
            function = new FunctionToken(symbolPos, binaryToken.Rhs);
            binaryToken.Rhs = function;

            // We don't add the function, but it still becomes the new parent
            compiler.ParentTokens.Push(function);
        }

        // Keep parsing until we are closed
        while (!function.IsClosed) {
            if (!compiler.ParseNextToken()) {
                throw new ParserException(function, "Unclosed function");
            }
        }

        return true;
    }
}