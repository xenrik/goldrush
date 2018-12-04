public class ValueToken<T> : Token {
    public T Value { get; private set; }

    public ValueToken(T value) {
        this.Value = value;
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