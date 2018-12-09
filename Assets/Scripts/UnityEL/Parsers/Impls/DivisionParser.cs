using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionParser : SingleCharacterParser<DivisionToken> {
    public DivisionParser() : base('/') {
    }
}