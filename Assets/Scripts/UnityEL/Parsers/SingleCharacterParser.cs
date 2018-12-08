using System.Collections.Generic;

public abstract class SingleCharacterParser<T> : TokenParser where T : RawToken {
    private char requiredChar;

    public SingleCharacterParser(char requiredChar) {
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
                parent = CreateToken(pos, parent);
                pos = i;

                return true;
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