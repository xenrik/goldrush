using UnityEngine;
using UnityEditor;

public class CloseParser : SingleCharacterParser {
    public CloseParser(char symbol) : base(symbol) {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        int symbolPos = compiler.Pos;
        if (base.Parse(compiler)) {
            // We must have a closeable token as the parent.
            if (!(compiler.Parent is CloseableToken)) {
                // Reset the position
                compiler.Pos = symbolPos;
                throw new ParserException(symbolPos, $"Unexpected '{symbol}'");
            }

            // Close the token and pop the parent
            CloseableToken parent = (CloseableToken)compiler.Parent;
            if (parent.IsClosed) {
                // Reset the position
                compiler.Pos = symbolPos;
                throw new ParserException(compiler.Parent, "Has already been closed");
            }

            parent.Close();
            compiler.ParentTokens.Pop();
            return true;
        }

        return false;
    }
}