using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentifierToken : Token {
    public string Value { get; private set; }

    public IdentifierToken(string value) {
        this.Value = value;
    }

    public override bool Equals(object other) {
        if (!(other is IdentifierToken)) {
            return false;
        }

        IdentifierToken otherToken = (IdentifierToken)other;
        return Value.Equals(otherToken.Value);
    }

    public override int GetHashCode() {
        return Value.GetHashCode();
    }

    public override string ToString() {
        return "Identifier{" + Value + "}";
    }
}
