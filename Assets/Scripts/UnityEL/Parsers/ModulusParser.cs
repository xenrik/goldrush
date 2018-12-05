using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulusParser : SingleCharacterParser<ModulusToken> {
    public ModulusParser() : base('%') {
    }
}