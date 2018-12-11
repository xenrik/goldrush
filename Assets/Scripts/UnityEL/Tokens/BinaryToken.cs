using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class BinaryToken : TokenImpl {
    public TokenImpl Lhs { get; private set; }
    public TokenImpl Rhs { get; private set; }

    public BinaryToken() {
    }
    public BinaryToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position) {
        this.Lhs = lhs;
        this.Rhs = rhs;
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