using System.Collections.Generic;

public abstract class SingleCharacterParser<T> : TokenParser where T : RawToken {
    private char requiredChar;

    public SingleCharacterParser(char requiredChar) {
        this.requiredChar = requiredChar;
    }

    public virtual RawToken Parse(RawToken container, char[] chars, ref int pos) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == requiredChar) {
                pos = i;
                return CreateToken(container, pos);
            }

            break;
        }

        return null;
    }

    protected virtual RawToken CreateToken(RawToken container, int position) {
        T token = (T)System.Activator.CreateInstance(typeof(T), position, container);
        container.AddToken(token);

        return container;
    }
}