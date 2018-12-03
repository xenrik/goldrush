using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StringParser : TokenParser {
    public Token Consume(Stack<Token> tokenStack, char[] chars, ref int pos) {
        // Must have a " or '
        int i = pos;
        char terminator = (char)0;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (terminator == 0) {
                if (char.IsWhiteSpace(ch)) {
                    continue;
                }

                if (ch == '"') {
                    terminator = '"';
                } else if (ch == '\'') {
                    terminator = '\'';
                } else {
                    return null;
                }
            } else if (ch == terminator) {
                string s = new string(chars, pos, i - pos);
                s = s.Trim();
                s = s.Substring(1, s.Length - 2);

                pos = i;
                return new StringToken(s);
            }
        }

        if (terminator != 0) {
            throw new ParserException("Unmatched string terminator: " + terminator);
        } else {
            return null;
        }
    }
}