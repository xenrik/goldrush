using System.Text;

public abstract class BinaryToken : TokenImpl {
    public TokenImpl Lhs { get; set; }
    public TokenImpl Rhs { get; set; }

    public BinaryToken() {
    }
    public BinaryToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position) {
        this.Lhs = lhs;
        this.Rhs = rhs;
    }
    
    public override int GetHashCode() {
        const int PRIME = 37;
        int hashCode = base.GetHashCode();
        hashCode = PRIME * hashCode + (Lhs != null ? Lhs.GetHashCode() : 0);
        hashCode = PRIME * hashCode + (Rhs != null ? Rhs.GetHashCode() : 0);

        return hashCode;
    }

    public override bool Equals(object obj, bool includeChildren) {
        if (!base.Equals(obj, includeChildren)) {
            return false;
        }

        BinaryToken other = (BinaryToken)obj;

        if (Lhs != null) {
            if (!Lhs.Equals(other.Lhs)) {
                return false;
            }
        } else if (other.Lhs != null) {
            return false;
        }

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
        buffer.AppendLine($"Lhs={(Lhs != null ? Lhs.ToString() : "null")},");
        buffer.AppendLine($"Rhs={(Rhs != null ? Rhs.ToString() : "null")}");

        return buffer.ToString();
    }
}