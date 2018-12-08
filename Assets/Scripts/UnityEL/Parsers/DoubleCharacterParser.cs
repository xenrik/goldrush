using System.Collections.Generic;

public abstract class DoubleCharacterParser<T> : TokenParser where T : RawToken {
    private char requiredChar;

    public DoubleCharacterParser(char requiredChar) {
        this.requiredChar = requiredChar;
    }

    public virtual bool Parse(char[] chars, ref int pos, ref RawToken parent) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == requiredChar) {
                if (i < chars.Length && chars[i] == requiredChar) {
                    parent = CreateToken(pos, parent);
                    pos = i + 1;

                    return true;
                }
            }

            break;
        }

        return false;
    }

    protected virtual RawToken CreateToken(int position, RawToken parent) {
        System.Activator.CreateInstance(typeof(T), position, parent);

        // Don't change the parent by default
        return parent;
    }
}