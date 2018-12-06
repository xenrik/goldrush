using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/**
 * Parser that searches for a " or ' and then consumes all characters up until the
 * next (non-escaped) " or '. Leading spaces are ignored.
 */
public class StringParser : TokenParser {

    public RawToken Consume(char[] chars, ref int pos) {
        // Must have a " or '
        int i = pos;
        char terminator = (char)0;
        char ch;
        bool lastWasEscape = false;
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
            } else if (ch == '\\') {
                lastWasEscape = !lastWasEscape;
            } else if (ch == terminator && !lastWasEscape) {
                string s = new string(chars, pos, i - pos);
                // Remove whitespace around the quotes
                s = s.Trim();

                // Remove the quotes
                s = s.Substring(1, s.Length - 2);

                // Replace escape sequences
                s = ReplaceEscapes(s);

                pos = i;
                return new StringToken(s);
            } else {
                lastWasEscape = false;
            }
        }

        if (terminator != 0) {
            throw new ParserException("Unmatched string terminator: " + terminator);
        } else {
            return null;
        }
    }

    /**
     * Replaces escape sequences within the string
     */
    private string ReplaceEscapes(string s) {
        return s.Replace("\\n", "\n")
            .Replace("\\\"", "\"")
            .Replace("\\'", "'");
    }
}