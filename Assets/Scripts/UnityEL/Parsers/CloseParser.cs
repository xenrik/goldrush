using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseParser : TokenParser {
    public CloseParser() {
    }

    public virtual bool Parse(char[] chars, ref int pos, ref RawToken parent) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == ')' || ch == ']') {
                CreateToken(parent, pos);
                pos = i;

                // Change the parent to our parent's parent
                parent = parent.Parent;
                return true;
            }

            break;
        }

        return false;
    }

    private void CreateToken(RawToken parent, int pos) {
        CloseToken token = new CloseToken(pos, parent);

        // The current parent must be closeable
        if (!(parent is CloseableToken)) {
            throw new ParserException(token, "Cannot close a non-closeable token: " + parent.DebugName);
        }

        // Close the parent
        try {
            ((CloseableToken)parent).Close();
        } catch (ParserException ex) {
            throw new ParserException(token, "Failed to close token", ex);
        }
    }
}