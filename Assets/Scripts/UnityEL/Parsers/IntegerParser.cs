using System.Collections.Generic;

/**
* Parser that accepts integer sequences. 
*/
public class IntegerParser : TokenParser {
    public bool Parse(char[] chars, ref int pos, ref RawToken parent) {
        int i = pos;
        int start = -1;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (start == -1) {
                if (char.IsWhiteSpace(ch)) {
                    continue;
                } else if (char.IsDigit(ch)) {
                    start = i - 1;
                } else {
                    return false;
                }
            } else if (!char.IsDigit(ch)) {
                --i;
                break;
            }
        }

        if (start != -1) {
            string s = new string(chars, start, i - start);
            s = s.Trim();

            try {
                new IntegerToken(int.Parse(s), pos, parent);
                pos = i;

                return true;
            } catch (System.FormatException e) {
                throw new ParserException("integer", pos, "Unable to parse as int: " + s, e);
            }
        }

        return false;
    }
}