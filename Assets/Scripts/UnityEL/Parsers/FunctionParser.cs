using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionParser : SingleCharacterParser<FunctionToken> {
    public FunctionParser() : base('(') {
    }

    protected override RawToken CreateToken(RawToken container, int position) {
        FunctionToken token = new FunctionToken(position);
        container.AddToken(token);

        // Return our new token as the container
        return token;
    }
}
