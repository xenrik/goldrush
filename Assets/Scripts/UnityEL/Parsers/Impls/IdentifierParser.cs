using System.Collections.Generic;

/**
 * Parser that accepts identifier like strings. Must start with an alpha char and will
 * then consume up until it encounters the first non-alpha, non-numeric char.
 */
public class IdentifierParser : TokenParser {
    public bool Parse(char[] chars, ref int pos, ref RawToken parent) {
        int i = pos;
        int start = -1;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

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
            string s = new string(chars, start, i - start);
            s = s.Trim();

            new IdentifierToken(s, pos, parent);
            pos = i;

            return true;
        }

        return false;
    }
}
