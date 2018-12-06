using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOrFunctionParser : SingleCharacterParser<GroupToken> {
    public GroupOrFunctionParser() : base('(') {
    }
}