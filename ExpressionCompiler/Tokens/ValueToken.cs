using System.Collections.Generic;

public interface ValueToken : Token, ExistsSupport {
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

    public override object Evaluate(UnityELEvaluator context) {
        return Value;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        if (Value != null) {
            return base.GetHashCode() * PRIME + Value.GetHashCode();
        } else {
            return base.GetHashCode();
        }
    }

    public override bool Equals(object other, bool includeChildren) {
        if (!base.Equals(other, includeChildren)) { 
            return false;
        }

        ValueTokenImpl<T> otherToken = (ValueTokenImpl<T>)other;
        if (Value != null) {
            return Value.Equals(otherToken.Value);
        } else {
            return otherToken.Value == null;
        }
    }

    protected override string GetTokenDataString() {
        return Value == null ? "null" : Value.ToString();
    }

    public virtual bool Exists(UnityELEvaluator context) {
        return true;
    }
}