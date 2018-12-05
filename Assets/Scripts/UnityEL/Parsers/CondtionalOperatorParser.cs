using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalOperatorParser : SingleCharacterParser<ConditionalOperatorToken> {
    public ConditionalOperatorParser() : base('?') {
    }
}