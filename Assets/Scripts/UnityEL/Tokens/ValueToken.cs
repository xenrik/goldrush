using System.Collections.Generic;

public interface ValueToken : Token {
    object Value { get; }
}

public abstract class ValueTokenImpl<T> : TokenImpl, ValueToken {
    public T Value { get; private set; }
    object ValueToken.Value { get { return Value; } }

    public ValueTokenImpl(T value) : base() {
        this.Value = value;
    }

    public ValueTokenImpl(T value, int position) : base(position) {
        this.Value = value;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        return base.GetHashCode() * PRIME + Value.GetHashCode();
    }

    public override bool Equals(object other, bool includeChildren) {
        if (!base.Equals(other, includeChildren)) { 
            return false;
        }

        ValueTokenImpl<T> otherToken = (ValueTokenImpl<T>)other;
        return Value.Equals(otherToken.Value);
    }

    protected override string GetTokenDataString() {
        return Value == null ? "null" : Value.ToString();
    }
}