using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionParser : SingleCharacterParser<FunctionToken> {
    public FunctionParser() : base('(') {
    }
}
