using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseParser : TokenParser {
    public CloseParser() {
    }

    public virtual RawToken Parse(RawToken container, char[] chars, ref int pos) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == ')' || ch == ']') {
                pos = i;

                return CreateToken(container, pos);
            }

            break;
        }

        return null;
    }

    private RawToken CreateToken(RawToken container, int pos) {
        CloseToken token = new CloseToken(pos);
        container.AddToken(token);

        // The current container must be closeable
        if (!(container is CloseableToken)) {
            throw new ParserException("Cannot close a non-closeable token", pos);
        }

        // Close the container and return its parent as the new container
        ((CloseableToken)container).Close();
        return container.Parent;
    }
}