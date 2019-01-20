using System;
using System.Collections.Generic;

/**
* Parser that accepts binary integer sequences (0b0101010). 
*/
public class BinaryIntegerParser : TokenParser {
    public bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        int start = -1;
        char ch;
        while (i < compiler.Expression.Length) {
            ch = compiler.Expression[i++];

            if (start == -1) {
                if (char.IsWhiteSpace(ch)) {
                    continue;
                } else if (ch == '0') {
                    if (i + 1 >= compiler.Expression.Length) {
                        return false;
                    }

                    char ch2 = compiler.Expression[i++];
                    char ch3 = compiler.Expression[i];

                    if (ch2 != 'b' || (ch3 != '0' && ch3 != '1')) {
                        return false;
                    }

                    start = i;
                } else {
                    return false;
                }
            } else if (ch != '0' && ch != '1') {
                --i;
                break;
            }
        }

        if (start != -1) {
            string s = new string(compiler.Expression, start, i - start);
            s = s.Trim();

            try {
                int value = Convert.ToInt32(s, 2);
                IntegerToken token = new IntegerToken(value, compiler.Pos, 2);

                compiler.Parent.AddChild(token);
                compiler.Pos = i;

                return true;
            } catch (System.FormatException e) {
                throw new ParserException("binaryInteger", compiler.Pos, "Unable to parse as binary int: " + s, e);
            }
        }

        return false;
    }
}