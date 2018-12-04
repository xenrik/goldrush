using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyAccessorParser : TokenParser {
    public Token Consume(Stack<Token> tokenStack, char[] chars, ref int pos) {
        // The tail of the stack must be an identifier
        if (tokenStack.Count == 0 || !(tokenStack.Peek() is IdentifierToken)) {
            return null;
        }

        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == '.') {
                pos = i;
                return new PropertyAccessorToken();
            }

            break;
        }

        return null;
    }
}