using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicationParser : SingleCharacterParser<MultiplicationToken> {
    public MultiplicationParser() : base('*') {
    }
}