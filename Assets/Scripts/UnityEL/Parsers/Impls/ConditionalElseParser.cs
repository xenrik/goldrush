using UnityEngine;
using UnityEditor;

public class ConditionalElseParser : SingleCharacterParser<ConditionalElseToken> {
    public ConditionalElseParser() : base(':') {
    }
}