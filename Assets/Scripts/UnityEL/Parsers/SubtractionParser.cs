using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtractionParser : SingleCharacterParser<SubtractionToken> {
    public SubtractionParser() : base('-') {
    }
}