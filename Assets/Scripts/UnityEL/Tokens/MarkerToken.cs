using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class MarkerToken : RawToken {
    public Token Resolve(Stack<RawToken> rawTokens) {
        return this;
    }
}