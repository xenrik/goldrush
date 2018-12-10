﻿using System.Collections.Generic;
using UnityEngine;

/**
* Parser that accepts boolean sequences. 
*/
public class BooleanParser : TokenParser {
    public bool Parse(char[] chars, ref int pos, ref RawToken parent) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == 't') {
                if (i + 3 <= chars.Length &&
                    new string(chars, i-1, 4).Equals("true")) {
                    new BooleanToken(true, pos, parent);
                    pos = i + 3;

                    return true;
                }
            } else if (ch == 'f') {
                if (i + 4 <= chars.Length &&
                    new string(chars, i-1, 5).Equals("false")) {
                    new BooleanToken(false, pos, parent);
                    pos = i + 4;

                    return true;
                }
            }

            break;
        }

        return false;
    }
}