using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotParser : SingleCharacterParser<NotToken> {
    public NotParser() : base('!') {
    }
}