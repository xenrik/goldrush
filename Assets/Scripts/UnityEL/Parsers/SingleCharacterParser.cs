using System.Collections.Generic;

public abstract class SingleCharacterParser<T> : TokenParser where T : Token, new() {
    private char requiredChar;

    public SingleCharacterParser(char requiredChar) {
        this.requiredChar = requiredChar;
    }

    public virtual Token Consume(Stack<Token> tokenStack, char[] chars, ref int pos) {
        int i = pos;
        char ch;
        while (i < chars.Length) {
            ch = chars[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == requiredChar) {
                pos = i;
                return new T();
            }

            break;
        }

        return null;
    }
}