﻿using System.Reflection;

public class PropertyAccessToken : TokenImpl, ExistsSupport, AssignableToken, HostSupport {
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
        PropertyDetails details = ResolveProperty(context);
        if (details == null) {
            return null;
        } else if (details.Property == null) {
            throw new NoSuchPropertyException(this, $"Property: {details.Name} not found on type: {details.HostType}");
        }

        return details.Property.GetValue(details.Host);
    }

    public bool Exists(UnityELEvaluator context) {
        PropertyDetails details = ResolveProperty(context);
        return details != null && details.Property != null;
    }

    public void Assign(UnityELEvaluator context, object value) {
        PropertyDetails details = ResolveProperty(context);
        if (details == null) {
            throw new ParserException(this, $"Did not resolve host object: {Host}");
        } else if (details.Property == null) {
            throw new NoSuchPropertyException(this, $"Property: {details.Name} not found on type: {details.HostType}");
        }

        if (!details.Property.CanWrite || details.Property.SetMethod == null ||
                details.Property.SetMethod.IsPrivate) {
            throw new ParserException(this, $"Property: {details.Name} on type: {details.HostType} is read only");
        }

        System.Type propertyType = details.Property.PropertyType;
        object coercedValue = TypeCoercer.CoerceToType(propertyType, this, value);
        details.Property.SetValue(details.Host, coercedValue);
    }

    private PropertyDetails ResolveProperty(UnityELEvaluator context) {
        PropertyDetails details = new PropertyDetails();
        details.Host = Host.Evaluate(context);
        if (details.Host == null) {
            return null;
        }

        details.Name = Property.Value;
        details.HostType = details.Host.GetType();
        details.Property = details.HostType.GetProperty(details.Name);
        
        return details;
    }

    private class PropertyDetails {
        public string Name;

        public object Host;
        public System.Type HostType;

        public PropertyInfo Property;
    }
}