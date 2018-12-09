using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCoalesceParser : DoubleCharacterParser<NullCoalesceToken> {
    public NullCoalesceParser() : base('?') {
    }
}
