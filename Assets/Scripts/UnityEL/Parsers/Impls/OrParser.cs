using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrParser : DoubleCharacterParser<OrToken> {
    public OrParser() : base('|') {
    }
}