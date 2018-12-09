using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupParser : SingleCharacterParser<GroupToken> {
    public GroupParser() : base('(') {
    }
}