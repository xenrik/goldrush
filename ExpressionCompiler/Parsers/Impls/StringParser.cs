/**
 * Parser that searches for a " or ' and then consumes all characters up until the
 * next (non-escaped) " or '. Leading spaces are ignored.
 */
public class StringParser : TokenParser {
    public bool Parse(ExpressionCompiler compiler) {
        // Must have a " or '
        int i = compiler.Pos;
        char terminator = (char)0;
        int terminatorPos = 0;
        char ch;
        bool lastWasEscape = false;
        while (i < compiler.Expression.Length) {
            ch = compiler.Expression[i++];

            if (terminator == 0) {
                if (char.IsWhiteSpace(ch)) {
                    continue;
                }

                if (ch == '"') {
                    terminatorPos = i - 1;
                    terminator = '"';
                } else if (ch == '\'') {
                    terminatorPos = i - 1;
                    terminator = '\'';
                } else {
                    return false;
                }
            } else if (ch == '\\') {
                lastWasEscape = !lastWasEscape;
            } else if (ch == terminator && !lastWasEscape) {
                string s = new string(compiler.Expression, compiler.Pos, i - compiler.Pos);
                // Remove whitespace around the quotes
                s = s.Trim();

                // Remove the quotes
                s = s.Substring(1, s.Length - 2);

                // Replace escape sequences
                s = ReplaceEscapes(s);

                StringToken token = new StringToken(s, compiler.Pos);
                compiler.Parent.AddChild(token);
                compiler.Pos = i;

                return true;
            } else {
                lastWasEscape = false;
            }
        }

        if (terminator != 0) {
            throw new ParserException("string", terminatorPos, "Unmatched string terminator: " + terminator);
        } else {
            return false;
        }
    }

    /**
     * Replaces escape sequences within the string
     */
    private string ReplaceEscapes(string s) {
        return s.Replace("\\n", "\n")
            .Replace("\\\"", "\"")
            .Replace("\\'", "'");
    }
}