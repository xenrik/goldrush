using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RootToken : RawToken {
    public override string Name { get { return "root"; } }

    public RootToken() : base() {
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        throw new System.NotImplementedException();
    }
}