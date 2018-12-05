using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XorParser : SingleCharacterParser<XorToken> {
    public XorParser() : base('^') {
    }
}