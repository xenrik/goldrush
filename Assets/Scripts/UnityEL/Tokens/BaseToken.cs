using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class BaseToken : RawToken {
    public virtual Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        throw new System.NotImplementedException();
    }
}