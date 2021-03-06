﻿using System.Collections.Generic;

/**
 * This parser does not create a token by default. It is intended to be 
 * used as a symbol parser for BinaryTokenParser but could be subclassed to
 * provide a token
 * 
 * Note the Pos on the compiler will have been advanced if this returns 
 * true, so if you need the statring position of the symbol ensure you 
 * capture it before you ask this to parse.
 */
public class SingleCharacterParser : TokenParser {
    protected char symbol;

    public SingleCharacterParser(char symbol) {
        this.symbol = symbol;
    }

    public virtual bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;
        char ch;
        while (i < compiler.Expression.Length) {
            ch = compiler.Expression[i++];

            if (char.IsWhiteSpace(ch)) {
                continue;
            } else if (ch == symbol) {
                compiler.Pos = i;
                return true;
            }

            break;
        }

        return false;
    }
}