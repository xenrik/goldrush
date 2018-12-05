using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionParser : SingleCharacterParser<AdditionToken> {
    public AdditionParser() : base('+') {
    }
}