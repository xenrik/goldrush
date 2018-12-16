using UnityEngine;
using UnityEditor;
using System.Reflection;

public class PropertyAccessToken : TokenImpl {
    public override string Name { get { return "propertyAccess"; } }

    // The host of the property we are returning
    public TokenImpl Host { get; private set; }

    // The property to return
    public IdentifierToken Property { get; private set; }

    public PropertyAccessToken() {
    }

    public PropertyAccessToken(int position, TokenImpl host, IdentifierToken property) : base(position) {
        this.Host = host;
        this.Property = property;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        int hashCode = base.GetHashCode();
        hashCode = PRIME * hashCode + (Host != null ? Host.GetHashCode() : 0);
        hashCode = PRIME * hashCode + (Property != null ? Property.GetHashCode() : 0);

        return hashCode;
    }

    public override bool Equals(object obj, bool includeChildren) {
        if (!base.Equals(obj, includeChildren)) {
            return false;
        }

        PropertyAccessToken other = (PropertyAccessToken)obj;

        if (Host != null) {
            if (!Host.Equals(other.Host)) {
                return false;
            }
        } else if (other.Host != null) {
            return false;
        }

        if (Property != null) {
            if (!Property.Equals(other.Property)) {
                return false;
            }
        } else if (other.Property != null) {
            return false;
        }

        return true;
    }

    protected override string GetTokenDataString() {
        return $"Host={(Host != null ? Host.ToString() : "null")}," +
            $"Property={(Property != null ? Property.ToString() : "null")}";
    }

    public override object Evaluate(UnityELEvaluator context) {
        object host = Host.Evaluate(context);
        if (host == null) {
            return null;
        }

        string name = Property.Value;
        System.Type hostType = host.GetType();

        PropertyInfo info = hostType.GetProperty(name);
        if (info == null) {
            throw new NoSuchPropertyException(this, $"Property: {name} not found on type: {hostType}");
        }

        return info.GetValue(host);
    }
}