using System.Collections.Generic;
using UnityEngine;

/**
* Parser that accepts boolean sequences. 
*/
public class BooleanParser : TokenParser {
    public bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        char ch;
        while (i < compiler.Expression.Length) {
            ch = compiler.Expression[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == 't') {
                if (i + 3 <= compiler.Expression.Length &&
                    new string(compiler.Expression, i - 1, 4).Equals("true")) {

                    BooleanToken token = new BooleanToken(true, compiler.Pos);
                    compiler.Parent.AddChild(token);
                    compiler.Pos = i + 3;

                    return true;
                }
            } else if (ch == 'f') {
                if (i + 4 <= compiler.Expression.Length &&
                    new string(compiler.Expression, i - 1, 5).Equals("false")) {

                    BooleanToken token = new BooleanToken(false, compiler.Pos);
                    compiler.Parent.AddChild(token);
                    compiler.Pos = i + 4;

                    return true;
                }
            }

            break;
        }

        return false;
    }
}
