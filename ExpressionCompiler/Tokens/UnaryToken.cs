using System.Text;

public abstract class UnaryToken : TokenImpl {
    public TokenImpl Rhs { get; set; }

    public UnaryToken() {
    }
    public UnaryToken(int position, TokenImpl rhs) : base(position) {
        this.Rhs = rhs;
    }
    
    public override int GetHashCode() {
        const int PRIME = 37;
        int hashCode = base.GetHashCode();
        hashCode = PRIME * hashCode + (Rhs != null ? Rhs.GetHashCode() : 0);

        return hashCode;
    }

    public override bool Equals(object obj, bool includeChildren) {
        if (!base.Equals(obj, includeChildren)) {
            return false;
        }

        UnaryToken other = (UnaryToken)obj;
        if (Rhs != null) {
            if (!Rhs.Equals(other.Rhs)) {
                return false;
            }
        } else if (other.Rhs != null) {
            return false;
        }

        return true;
    }

    protected override string GetTokenDataString() {
        StringBuilder buffer = new StringBuilder();
        buffer.AppendLine();
        buffer.AppendLine($"Rhs={(Rhs != null ? Rhs.ToString() : "null")}");

        return buffer.ToString();
    }
}