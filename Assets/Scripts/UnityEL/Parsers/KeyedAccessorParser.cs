using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyedAccessorParser : SingleCharacterParser<KeyedAccessorToken> {
    public KeyedAccessorParser() : base('[') {
    }
}