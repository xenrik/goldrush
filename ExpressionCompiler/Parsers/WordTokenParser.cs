using System;

/**
 * A token parser that matches whole words (i.e. each word
 * must be followed by at least one whitespace character). 
 * 
 * The last word can also be followed by the end of the expression or a non-alphanumeric
 * character.
 * 
 * This can be used a symbol parser if required, or subclassed to behave as a
 * normal token parser. Note the Pos on the compiler will have been advanced 
 * if this returns  true, so if you need the statring position of the symbol
 * ensure you  capture it before you ask this to parse.
 */
public class WordTokenParser : TokenParser {
    private string[] words;
    private bool ignoreCase;

    /**
     * Constructor accepting the words to search for
     */
    public WordTokenParser(bool ignoreCase, params string[] words) {
        this.words = words;
        this.ignoreCase = ignoreCase;
    }

    public virtual bool Parse(ExpressionCompiler compiler) {
        int startPos = compiler.Pos;
        for (int i = 0; i < words.Length; ++i) {
            if (!ParseWord(compiler, words[i], i + 1 == words.Length)) {
                compiler.Pos = startPos;
                return false;
            }
        }

        return true;
    }

    private bool ParseWord(ExpressionCompiler compiler, string word, bool isLastWord) { 
        int i = compiler.Pos;

        // Advance to the next non-whitespace character
        while (i < compiler.Expression.Length &&
            char.IsWhiteSpace(compiler.Expression[i])) {
            ++i;
        }

        // If we don't have enough characters left we definately can't match
        if (i + word.Length > compiler.Expression.Length) {
            return false;
        }

        // Check if the word we found matches
        string foundWord = new string(compiler.Expression, i, word.Length);
        if (ignoreCase) {
            if (!foundWord.Equals(word, StringComparison.InvariantCultureIgnoreCase)) {
                return false;
            }
        } else if (!foundWord.Equals(word)) {
            return false;
        }

        // Check we're at the end of the expression, or that there is a suitable following character
        int nextCharPos = i + word.Length;
        bool accept = false;
        if (nextCharPos >= compiler.Expression.Length) {
            accept = isLastWord;
        } else {
            char nextCh = compiler.Expression[nextCharPos];
            accept = char.IsWhiteSpace(nextCh);
            accept |= isLastWord && !char.IsLetterOrDigit(nextCh);
        }

        if (accept) {
            compiler.Pos = i + word.Length;
            return true;
        } else {
            return false;
        }
    }
}
