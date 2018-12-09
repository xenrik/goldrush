using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionParser : SingleCharacterParser<FunctionToken> {
    public FunctionParser() : base('(') {
    }

    protected override RawToken CreateToken(int position, RawToken parent) {
        // Return our new token as the new parent
        return new FunctionToken(position, parent);
    }
}
