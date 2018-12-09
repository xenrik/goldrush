using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOrFunctionParser : SingleCharacterParser<GroupOrFunctionToken> {
    public GroupOrFunctionParser() : base('(') {
    }
}