using System.Collections.Generic;

public abstract class ValueToken<T> : RawToken {
    public T Value { get; private set; }

    public ValueToken(T value) : base() {
        this.Value = value;
    }

    public ValueToken(T value, int position, RawToken parent) : base(position, parent) {
        this.Value = value;
    }

    public override Token Resolve(Stack<RawToken> rawTokens, Stack<Token> resolvedTokens) {
        return this;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        return base.GetHashCode() * PRIME + Value.GetHashCode();
    }

    public override bool Equals(object other, bool includeChildren) {
        if (!base.Equals(other, includeChildren)) { 
            return false;
        }

        ValueToken<T> otherToken = (ValueToken<T>)other;
        return Value.Equals(otherToken.Value);
    }

    protected override string GetTokenDataString() {
        return Value == null ? "null" : Value.ToString();
    }
}