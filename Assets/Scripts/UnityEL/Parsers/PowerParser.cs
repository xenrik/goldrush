using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerParser : SingleCharacterParser<PowerToken> {
    public PowerParser() : base('^') {
    }

    public override Token Consume(Stack<Token> tokenStack, char[] chars, ref int pos) {
        // The tail of the stack must be an integer or decimal
        if (tokenStack.Count == 0) {
            return null;
        }

        Token token = tokenStack.Peek();
        if (token is IntegerToken || token is DecimalToken) {
            return base.Consume(tokenStack, chars, ref pos);
        } else {
            return null;
        }
    }
}
