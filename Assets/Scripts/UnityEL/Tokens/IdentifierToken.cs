﻿public class IdentifierToken : ValueToken<string> {
    public IdentifierToken(string value) : base(value) {
    }

    public override string ToString() {
        return "Identifier{" + Value + "}";
    }
}
