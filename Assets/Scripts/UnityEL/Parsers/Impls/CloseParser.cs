using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseParser : TokenParser {
    public CloseParser() {
    }

    public virtual bool Parse(char[] chars, ref int pos, ref RawToken parent) {
        // Nothing to do if the current token is not closeable
        if (!(parent is CloseableToken)) {
            return false;
        }

        CloseableToken closeableParent = (CloseableToken)parent;
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == closeableParent.CloseTokenSymbol) {
                CloseParentToken(parent, pos);
                pos = i;

                // Change the parent to our parent's parent
                parent = parent.Parent;
                return true;
            }

            break;
        }

        return false;
    }

    private void CloseParentToken(RawToken parent, int pos) {
        // Add a close token for the resolve phase
        CloseToken token = new CloseToken(pos, parent);

        // Tell the parent we have close parsing for it
        try {
            ((CloseableToken)parent).ClosedParsing();
        } catch (ParserException ex) {
            throw new ParserException(token, "Failed to close token", ex);
        }
    }
}