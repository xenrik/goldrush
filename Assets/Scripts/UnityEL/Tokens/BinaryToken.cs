using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class BinaryToken : RawToken {
    public Token Lhs { get; private set; }
    public Token Rhs { get; private set; }

    public BinaryToken() {
    }
    public BinaryToken(int position, RawToken parent) : base(position, parent) {
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        // Take the next raw token as the LHS, and the current resolvedToken as the RHS
        if (rawTokens.Count == 0 || resolvedTokens.Count == 0) {
            throw new ParserException(this, "Missing operand");
        }

        Lhs = rawTokens.Pop().Resolve(rawTokens, resolvedTokens);
        Rhs = resolvedTokens.Pop();

        return this;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        int hashCode = base.GetHashCode();
        hashCode = PRIME * hashCode + (Lhs != null ? Lhs.GetHashCode() : 0);
        hashCode = PRIME * hashCode + (Rhs != null ? Rhs.GetHashCode() : 0);

        return hashCode;
    }

    public override bool Equals(object obj, bool includeChildren) {
        if (!base.Equals(obj, includeChildren)) {
            return false;
        }

        BinaryToken other = (BinaryToken)obj;

        if (Lhs != null) {
            if (!Lhs.Equals(other.Lhs)) {
                return false;
            }
        } else if (other.Lhs != null) {
            return false;
        }

        if (Rhs != null) {
            if (!Rhs.Equals(other.Rhs)) {
                return false;
            }
        } else if (other.Rhs != null) {
            return false;
        }

        return true;
    }

    protected override string GetTokenDataString() {
        return $"Lhs={(Lhs != null ? Lhs.ToString() : "null")}," +
            $"Rhs={(Rhs != null ? Rhs.ToString() : "null")}";
    }
}