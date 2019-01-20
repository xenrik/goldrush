using System.Collections.Generic;

/**
 * Parser that accepts identifier like strings. Must start with an alpha char and will
 * then consume up until it encounters the first non-alpha, non-numeric char.
 */
public class IdentifierParser : TokenParser {
    public bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        int start = -1;
        char ch;
        while (i < compiler.Expression.Length) {
            ch = compiler.Expression[i++];

            if (start == -1) {
                if (char.IsWhiteSpace(ch)) {
                    continue;
                } else if (char.IsLetter(ch)) {
                    start = i - 1;
                } else {
                    return false;
                }
            } else if (!char.IsLetterOrDigit(ch)) {
                --i;
                break;
            }
        }

        if (start != -1) {
            string s = new string(compiler.Expression, start, i - start);
            s = s.Trim();

            IdentifierToken token = new IdentifierToken(s, compiler.Pos);
            compiler.Parent.AddChild(token);
            compiler.Pos = i;

            return true;
        }

        return false;
    }
}