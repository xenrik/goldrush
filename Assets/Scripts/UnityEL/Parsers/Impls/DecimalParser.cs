using System.Collections.Generic;

/**
* Parser that accepts decimal sequences. Does not accept integer sequences!
* Does not accept decimals that do not have a digit before the period
*/
public class DecimalParser : TokenParser {
    public bool Parse(char[] chars, ref int pos, ref RawToken parent) {
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
                    return false;
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

            try {
                DecimalToken token = new DecimalToken(float.Parse(s), pos, parent);
                pos = i;

                return true;
            } catch (System.FormatException e) {
                throw new ParserException("decimal", pos, "Unable to parse as decimal: " + s, e);
            }
        }

        return false;
    }
}
