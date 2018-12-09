using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplementParser : SingleCharacterParser<ComplementToken> {
    public ComplementParser() : base('~') {
    }
}