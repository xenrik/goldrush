using System.Collections.Generic;

/**
* Parser that accepts integer sequences. 
*/
public class IntegerParser : TokenParser {
    public bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        int start = -1;
        char ch;
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
            } else if (!char.IsDigit(ch)) {
                --i;
                break;
            }
        }

        if (start != -1) {
            string s = new string(compiler.Expression, start, i - start);
            s = s.Trim();

            try {
                IntegerToken token = new IntegerToken(int.Parse(s), compiler.Pos);
                compiler.Parent.AddChild(token);
                compiler.Pos = i;

                return true;
            } catch (System.FormatException e) {
                throw new ParserException("integer", compiler.Pos, "Unable to parse as int: " + s, e);
            }
        }

        return false;
    }
}