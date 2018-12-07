using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class BaseToken : RawToken {
    public int Position { get; private set; }

    public RawToken Parent { get; set; }

    /** The children of this token */
    private List<RawToken> childTokens;

    public BaseToken(int position) {
        this.Position = position;
    }

    public void AddToken(RawToken child) {
        if (childTokens == null) {
             childTokens = new List<RawToken>();
        }

        childTokens.Add(child);
        child.Parent = this;
    }

    public virtual Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        throw new System.NotImplementedException();
    }


}