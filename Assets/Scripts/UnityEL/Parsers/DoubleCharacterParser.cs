using System.Collections.Generic;

/**
 * This parser does not create a token. It is intended to be used as a 
 * symbol parser for BinaryTokenParser. 
 * 
 * Note the Pos on the compiler will have been advanced if this returns 
 * true, so if you need the statring position of the symbol ensure you 
 * capture it before you ask this to parse.
 */
public class DoubleCharacterParser : TokenParser {
    private char requiredChar;

    public DoubleCharacterParser(char requiredChar) {
        this.requiredChar = requiredChar;
    }

    public virtual bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        char ch;
        while (i < compiler.Expression.Length) {
            ch = compiler.Expression[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == requiredChar) {
                if (i < compiler.Expression.Length && compiler.Expression[i] == requiredChar) {
                    compiler.Pos = i + 1;
                    return true;
                }
            }

            break;
        }

        return false;
    }
}