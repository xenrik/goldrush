using System.Collections.Generic;

/**
* Parser that accepts decimal sequences. Does not accept integer sequences!
* Does not accept decimals that do not have a digit before the period
*/
public class DecimalParser : TokenParser {
    public RawToken Parse(char[] chars, ref int pos) {
        int i = pos;
        int start = -1;
        char ch;
        bool hasDecimal = false;
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
            } else if (char.IsDigit(ch)) {
                continue;
            } else if (ch == '.') {
                if (hasDecimal) {
                    --i;
                    break;
                } else {
                    hasDecimal = true;
                }
            } else {
                --i;
                break;
            }
        }

        if (start != -1 && hasDecimal) {
            string s = new string(chars, start, i - start);
            s = s.Trim();

            pos = i;
            try {
                return new DecimalToken(float.Parse(s));
            } catch (System.FormatException e) {
                throw new ParserException("Unable to parse as float: " + s, e);
            }
        }

        return null;
    }
}
