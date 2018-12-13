using UnityEngine;
using UnityEditor;

public class FunctionToken : TokenImpl {
    public override string Name { get { return "function"; } }
    public TokenImpl FunctionName { get; private set; }

    public FunctionToken() {
    }

    public FunctionToken(int position, TokenImpl functionName) : base(position) {
        this.FunctionName = functionName;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        if (FunctionName != null) {
            return base.GetHashCode() * PRIME + FunctionName.GetHashCode();
        } else {
            return base.GetHashCode();
        }
    }

    public override bool Equals(object other, bool includeChildren) {
        if (!base.Equals(other, includeChildren)) {
            return false;
        }

        FunctionToken otherToken = (FunctionToken)other;
        if (FunctionName != null) {
            return FunctionName.Equals(otherToken.FunctionName);
        } else {
            return otherToken.FunctionName == null;
        }
    }

    protected override string GetTokenDataString() {
        return FunctionName == null ? "null" : FunctionName.ToString();
    }
}