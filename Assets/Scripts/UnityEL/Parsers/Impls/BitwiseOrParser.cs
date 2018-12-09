using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitwiseOrParser : SingleCharacterParser<BitwiseOrToken> {
    public BitwiseOrParser() : base('|') {
    }
}