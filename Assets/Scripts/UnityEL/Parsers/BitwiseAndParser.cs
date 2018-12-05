using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitwiseAndParser : SingleCharacterParser<BitwiseAndToken> {
    public BitwiseAndParser() : base('&') {
    }
}