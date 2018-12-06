using System.Collections.Generic;

/**
* Parser that accepts integer sequences. 
*/
public class IntegerParser : TokenParser {
    public RawToken Consume(char[] chars, ref int pos) {
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
                    return null;
                }
            } else if (!char.IsDigit(ch)) {
                --i;
                break;
            }
        }

        if (start != -1) {
            string s = new string(chars, start, i - start);
            s = s.Trim();

            pos = i;
            try {
                return new IntegerToken(int.Parse(s));
            } catch (System.FormatException e) {
                throw new ParserException("Unable to parse as int: " + s, e);
            }
        }

        return null;
    }
}