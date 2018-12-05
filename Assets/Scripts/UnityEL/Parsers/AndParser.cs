using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndParser : DoubleCharacterParser<AndToken> {
    public AndParser() : base('&') {
    }
}