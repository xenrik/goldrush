using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class UnaryToken : RawToken {
    public Token Operand { get; private set; }

    public UnaryToken() {
    }
    public UnaryToken(int position, RawToken parent) : base(position, parent) {
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        // Take the current resolvedToken as the operand
        if (resolvedTokens.Count == 0) {
            throw new ParserException(this, "Missing operand");
        }

        Operand = resolvedTokens.Pop();
        return this;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        int hashCode = base.GetHashCode();
        hashCode = PRIME * hashCode + (Operand != null ? Operand.GetHashCode() : 0);

        return hashCode;
    }

    public override bool Equals(object obj, bool includeChildren) {
        if (!base.Equals(obj, includeChildren)) {
            return false;
        }

        UnaryToken other = (UnaryToken)obj;

        if (Operand != null) {
            if (!Operand.Equals(other.Operand)) {
                return false;
            }
        } else if (other.Operand != null) {
            return false;
        }

        return true;
    }

    protected override string GetTokenDataString() {
        return $"Operand={(Operand != null ? Operand.ToString() : "null")}";
    }
}