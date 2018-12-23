using System;
using System.Collections.Generic;

/**
* Parser that accepts hexadecimal integer sequences (0x0101010). 
*/
public class HexIntegerParser : TokenParser {
    private static HashSet<char> validChars = new HashSet<char> {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f' };

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

                    if (ch2 != 'x' || !validChars.Contains(ch3)) {
                        return false;
                    }

                    start = i;
                } else {
                    return false;
                }
            } else if (!validChars.Contains(ch)) {
                --i;
                break;
            }
        }

        if (start != -1) {
            string s = new string(compiler.Expression, start, i - start);
            s = s.Trim();

            try {
                int value = Convert.ToInt32(s, 16);
                IntegerToken token = new IntegerToken(value, compiler.Pos, 16);

                compiler.Parent.AddChild(token);
                compiler.Pos = i;

                return true;
            } catch (System.FormatException e) {
                throw new ParserException("hexadecimalInteger", compiler.Pos, "Unable to parse as hexadeimcal int: " + s, e);
            }
        }

        return false;
    }
}