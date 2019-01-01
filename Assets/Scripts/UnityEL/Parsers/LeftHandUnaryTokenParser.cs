using UnityEngine;
using UnityEditor;

public abstract class LeftHandUnaryTokenParser : TokenParser {

    public abstract string Name { get; }

    private TokenParser symbolParser;

    public LeftHandUnaryTokenParser(TokenParser symbolParser) {
        this.symbolParser = symbolParser;
    }

    public bool Parse(ExpressionCompiler compiler) {
        int symbolPos = compiler.Pos;
        if (!symbolParser.Parse(compiler)) {
            return false;
        }

        // Lenient by default
        if (compiler.Parent.Children.Count == 0) {
            // Reset the compiler position
            compiler.Pos = symbolPos;
            return false;
        }
        TokenImpl lhs = compiler.Parent.PopChild();

        TokenImpl token = CreateToken(compiler, symbolPos, lhs);
        if (token != null) {
            compiler.Parent.AddChild(token);
        }

        return true;
    }

    protected TokenImpl ApplyPrecedence(ExpressionCompiler compiler, BinaryToken lhs, LeftHandUnaryToken newToken) {
        // If the left-hand side is a binary token, and the new token has a higher precedence, 
        // then take the right-hand side from the old left-hand side and use that as the left
        // hand side on the new token. The new token then becomes the right-hand side of the left
        // hand side.
        int lhsPrecedence = compiler.GetPrecedence(lhs.GetType());
        int newPrecedence = compiler.GetPrecedence(newToken.GetType());

        if (newPrecedence < lhsPrecedence) {
            newToken.Lhs = lhs.Rhs;
            lhs.Rhs = newToken;

            return lhs;
        } else {
            return newToken;
        }
    }

    protected abstract TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs);
}