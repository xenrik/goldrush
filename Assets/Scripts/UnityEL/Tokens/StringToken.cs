using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringToken : Token {
    public string Value { get; private set; }

    public StringToken(string value) {
        this.Value = value;
    }

    public override bool Equals(object other) {
        if (!(other is StringToken)) {
            return false;
        }

        StringToken otherToken = (StringToken)other;
        return Value.Equals(otherToken.Value);
    }

    public override int GetHashCode() {
        return Value.GetHashCode();
    }

    public override string ToString() {
        return "String{" + Value + "}";
    }
}
