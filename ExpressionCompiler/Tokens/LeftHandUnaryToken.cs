using System.Text;

public abstract class LeftHandUnaryToken : TokenImpl {
    public TokenImpl Lhs { get; set; }

    public LeftHandUnaryToken() {
    }
    public LeftHandUnaryToken(int position, TokenImpl Lhs) : base(position) {
        this.Lhs = Lhs;
    }
    
    public override int GetHashCode() {
        const int PRIME = 37;
        int hashCode = base.GetHashCode();
        hashCode = PRIME * hashCode + (Lhs != null ? Lhs.GetHashCode() : 0);

        return hashCode;
    }

    public override bool Equals(object obj, bool includeChildren) {
        if (!base.Equals(obj, includeChildren)) {
            return false;
        }

        LeftHandUnaryToken other = (LeftHandUnaryToken)obj;
        if (Lhs != null) {
            if (!Lhs.Equals(other.Lhs)) {
                return false;
            }
        } else if (other.Lhs != null) {
            return false;
        }

        return true;
    }

    protected override string GetTokenDataString() {
        StringBuilder buffer = new StringBuilder();
        buffer.AppendLine();
        buffer.AppendLine($"Lhs={(Lhs != null ? Lhs.ToString() : "null")}");

        return buffer.ToString();
    }
}