using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/**
 * An token parser that matches a string of characters. 
 * 
 * This can be used a symbol parser if required, or subclassed to behave as a
 * normal token parser. Note the Pos on the compiler will have been advanced 
 * if this returns  true, so if you need the statring position of the symbol
 * ensure you  capture it before you ask this to parse.
 */
public class StringTokenParser : TokenParser {
    private string token;
    private bool ignoreCase;

    /**
     * Constructor accepting the words to search for
     */
    public StringTokenParser(string token, bool ignoreCase = false) {
        this.token = token;
        this.ignoreCase = ignoreCase;
    }

    public virtual bool Parse(ExpressionCompiler compiler) {
        int i = compiler.Pos;

        // Advance to the next non-whitespace character
        while (i < compiler.Expression.Length &&
            char.IsWhiteSpace(compiler.Expression[i])) {
            ++i;
        }

        // If we don't have enough characters left we definately can't match
        if (i + token.Length > compiler.Expression.Length) {
            return false;
        }

        // Check if the word we found matches
        string foundWord = new string(compiler.Expression, i, token.Length);
        if (ignoreCase) {
            if (!foundWord.Equals(token, StringComparison.InvariantCultureIgnoreCase)) {
                return false;
            }
        } else if (!foundWord.Equals(token)) {
            return false;
        }

        compiler.Pos = i + token.Length;
        return true;
    }
}
