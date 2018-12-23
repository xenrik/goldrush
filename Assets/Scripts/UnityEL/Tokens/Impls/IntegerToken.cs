public class IntegerToken : ValueTokenImpl<float> {
    public override string Name { get { return "integer"; } }

    // The radix used when the token was created. Only kept for information
    public int Radix { get; private set; }

    public IntegerToken(float value) : base(value) {
    }
    public IntegerToken(float value, int position, int radix = 10) : base(value, position) {
        this.Radix = radix;
    }

    public override int GetHashCode() {
        int prime = 31;
        return base.GetHashCode() * prime + Radix;
    }

    public override bool Equals(object other, bool includeChildren) {
        if (!base.Equals(other, includeChildren)) {
            return false;
        }

        IntegerToken integerToken = (IntegerToken)other;
        return Radix == integerToken.Radix;
    }

    protected override string GetTokenDataString() {
        if (Radix != 10) {
            return $"Radix={Radix},Value={Value}";
        } else {
            return base.GetTokenDataString();
        }
    }
}