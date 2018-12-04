using System.Collections.Generic;
using UnityEngine;

/**
* Parser that accepts boolean sequences. 
*/
public class BooleanParser : TokenParser {
    public Token Consume(Stack<Token> tokenStack, char[] chars, ref int pos) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == 't') {
                if (i + 3 <= chars.Length &&
                    new string(chars, i-1, 4).Equals("true")) {
                    pos = i + 3;
                    return new BooleanToken(true);
                }
            } else if (ch == 'f') {
                if (i + 4 <= chars.Length &&
                    new string(chars, i-1, 5).Equals("false")) {
                    pos = i + 4;
                    return new BooleanToken(false);
                }
            }

            break;
        }

        return null;
    }
}
