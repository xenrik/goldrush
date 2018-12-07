using System.Collections.Generic;

public abstract class DoubleCharacterParser<T> : TokenParser where T : RawToken, new() {
    private char requiredChar;

    public DoubleCharacterParser(char requiredChar) {
        this.requiredChar = requiredChar;
    }

    public virtual RawToken Parse(char[] chars, ref int pos) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == requiredChar) {
                if (i < chars.Length && chars[i] == requiredChar) {
                    pos = i + 1;
                    return new T();
                }
            }

            break;
        }

        return null;
    }
}