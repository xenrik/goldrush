using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseParser : TokenParser {
    public CloseParser() {
    }

    public virtual Token Consume(Stack<Token> tokenStack, char[] chars, ref int pos) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == ')' || ch == ']') {
                pos = i;
                return new CloseToken();
            }

            break;
        }

        return null;
    }
}