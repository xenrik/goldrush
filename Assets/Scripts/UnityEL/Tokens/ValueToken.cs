using System.Collections.Generic;

public abstract class ValueToken<T> : BaseToken {
    public T Value { get; private set; }

    public ValueToken(T value) {
        this.Value = value;
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        return this;
    }

    public override bool Equals(object other) {
        if (!(other is ValueToken<T>)) {
            return false;
        }

        ValueToken<T> otherToken = (ValueToken<T>)other;
        return Value.Equals(otherToken.Value);
    }

    public override int GetHashCode() {
        return Value.GetHashCode();
    }
}