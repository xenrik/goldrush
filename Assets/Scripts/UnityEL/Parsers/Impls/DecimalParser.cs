using System.Collections.Generic;

/**
* Parser that accepts decimal sequences. Does not accept integer sequences!
* Does not accept decimals that do not have a digit before the period
*/
public class DecimalParser : TokenParser {
    public bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        int start = -1;
        char ch;
        bool hasDecimal = false;
        while (i < compiler.Expression.Length) {
            ch = compiler.Expression[i++];

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
            string s = new string(compiler.Expression, start, i - start);
            s = s.Trim();

            try {
                DecimalToken token = new DecimalToken(float.Parse(s), compiler.Pos);
                compiler.Parent.AddChild(token);
                compiler.Pos = i + 4;

                return true;
            } catch (System.FormatException e) {
                throw new ParserException("decimal", compiler.Pos, "Unable to parse as decimal: " + s, e);
            }
        }

        return false;
    }
}
