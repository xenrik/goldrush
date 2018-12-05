using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionParser : SingleCharacterParser<FunctionToken> {
    public FunctionParser() : base('(') {
    }

    public override Token Consume(Stack<Token> tokenStack, char[] chars, ref int pos) {
        // The tail of the stack must be an identifier
        if (tokenStack.Count == 0 || !(tokenStack.Peek() is IdentifierToken)) {
            return null;
        }

        return base.Consume(tokenStack, chars, ref pos);
    }
}
