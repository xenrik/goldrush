using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgumentParser : SingleCharacterParser<ArgumentToken> {
    public ArgumentParser() : base(',') {
    }
}