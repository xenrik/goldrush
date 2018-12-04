using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgumentParser : TokenParser {
    public Token Consume(Stack<Token> tokenStack, char[] chars, ref int pos) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == ',') {
                pos = i;
                return new ArgumentToken();
            }

            break;
        }

        return null;
    }
}