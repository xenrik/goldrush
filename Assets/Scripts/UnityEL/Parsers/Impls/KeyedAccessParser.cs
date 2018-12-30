using UnityEngine;
using UnityEditor;

public class KeyedAccessParser : SingleCharacterParser {
    public KeyedAccessParser() : base('[') {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        int symbolPos = compiler.Pos;
        if (!base.Parse(compiler)) {
            return false;
        }

        // Must have a left-hand side...
        if (compiler.Parent.Children.Count == 0) {
            // Reset the compiler position
            compiler.Pos = symbolPos;
            throw new ParserException("keyedAccess", symbolPos, "Missing left-hand operand");
        }
        TokenImpl lhs = compiler.Parent.PopChild();

        // See if we can handle the lhs
        KeyedAccessToken keyedAccess = null;
        if (lhs is IdentifierToken || lhs is PropertyAccessToken) {
            keyedAccess = new KeyedAccessToken(symbolPos, lhs);
            compiler.Parent.AddChild(keyedAccess);
        } else if (lhs is BinaryToken) {
            // If the RHS of the binary token is an Identifier or Property Access we can join to and replace it
            BinaryToken binaryLhs = (BinaryToken)lhs;
            TokenImpl lhsRhs = binaryLhs.Rhs;

            if (lhsRhs is IdentifierToken || lhsRhs is PropertyAccessToken) {
                // We can join it
                keyedAccess = new KeyedAccessToken(symbolPos, lhsRhs);
                binaryLhs.Rhs = keyedAccess;

                compiler.Parent.AddChild(binaryLhs);
            }
        } else if (lhs is UnaryToken) {
            // If the RHS of the unary token is an Identifier or Property Access we can join to and replace it
            UnaryToken unaryLhs = (UnaryToken)lhs;
            TokenImpl lhsRhs = unaryLhs.Rhs;

            if (lhsRhs is IdentifierToken || lhsRhs is PropertyAccessToken) {
                // We can join it
                keyedAccess = new KeyedAccessToken(symbolPos, lhsRhs);
                unaryLhs.Rhs = keyedAccess;

                compiler.Parent.AddChild(unaryLhs);
            }
        }

        // Throw an exception if we couldn't cope with the lhs.
        if (keyedAccess == null) {
            // Restore the lhs and position
            compiler.Parent.AddChild(lhs);
            compiler.Pos = symbolPos;

            throw new ParserException("keyedAccess", symbolPos, $"Left-hand side of a keyed access token cannot be: {lhs.Name}");
        }

        // The keyed access becomes the new parent token
        compiler.ParentTokens.Push(keyedAccess);

        // Keep parsing until we are closed
        while (!keyedAccess.IsClosed) {
            if (!compiler.ParseNextToken()) {
                throw new ParserException(keyedAccess, "Unclosed keyed access");
            }
        }

        return true;
    }
}