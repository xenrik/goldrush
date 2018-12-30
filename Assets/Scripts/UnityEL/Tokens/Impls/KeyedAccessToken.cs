using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Collections;

public class KeyedAccessToken : TokenImpl, CloseableToken, ExistsSupport {
    public override string Name { get { return "keyedAccess"; } }
    public TokenImpl Host { get; private set; }
    public bool IsClosed { get; private set; }

    public KeyedAccessToken() {
    }

    public KeyedAccessToken(int position, TokenImpl hostToken) : base(position) {
        this.Host = hostToken;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        if (Host != null) {
            return base.GetHashCode() * PRIME + Host.GetHashCode();
        } else {
            return base.GetHashCode();
        }
    }

    public override bool Equals(object other, bool includeChildren) {
        if (!base.Equals(other, includeChildren)) {
            return false;
        }

        KeyedAccessToken otherToken = (KeyedAccessToken)other;
        if (Host != null) {
            return Host.Equals(otherToken.Host);
        } else {
            return otherToken.Host == null;
        }
    }

    protected override string GetTokenDataString() {
        return Host == null ? "null" : Host.ToString();
    }

    public void Close() {
        if (IsClosed) {
            throw new ParserException(this, "Has already been closed");
        }

        IsClosed = true;
    }

    public override void Validate() {
        if (!IsClosed) {
            throw new ParserException(this, "Has not been closed");
        }

        bool valieToken = false;
        valieToken |= Host is PropertyAccessToken;
        valieToken |= Host is IdentifierToken;

        if (!valieToken) {
            throw new ParserException(this, $"Unsupport token type for host: {Host}");
        }

        // We should have a single child.
        if (Children.Count > 1) {
            throw new ParserException(this, "Invalid expression (too many children)");
        } else if (Children.Count == 0) {
            throw new ParserException(this, "Invalid expression (no children)");
        }
    }

    public override object Evaluate(UnityELEvaluator context) {
        object host = Host.Evaluate(context);
        if (host == null) {
            return null;
        }
        System.Type hostType = host.GetType();

        object key = Children[0].Evaluate(context);
        System.Type keyType = key?.GetType();

        // If the key is a string, we need to see if there is a property on the host that matches
        if (key is string) {
            PropertyInfo info = hostType.GetProperty((string)key);
            if (info != null) {
                return info.GetValue(host);
            }
        }

        // Otherwise inspect the host to determine what to do
        if (host is IDictionary) {
            IDictionary dictionary = (IDictionary)host;
            if (dictionary.Contains(key)) {
                return dictionary[key];
            } else {
                return null;
            }
        } else if (host is IList) {
            IList list = (IList)host;
            int i = TypeCoercer.CoerceToType<int>(this, key);
            return list[i];
        } else if (host is Array) {
            Array array = (Array)host;
            int i = TypeCoercer.CoerceToType<int>(this, key);
            return array.GetValue(i);
        }

        throw new ParserException(this, $"Unsupported host value type: {hostType}, or unknown property: {key}");
    }

    /**
     * Although this shares a fair bit of code with Evaluate, it is easier to read
     * as a separate method
     */
    public bool Exists(UnityELEvaluator context) {
        object host = Host.Evaluate(context);
        if (host == null) {
            return false;
        }
        System.Type hostType = host.GetType();

        object key = Children[0].Evaluate(context);
        System.Type keyType = key?.GetType();

        // If the key is a string, we need to see if there is a property on the host that matches
        if (key is string) {
            PropertyInfo info = hostType.GetProperty((string)key);
            if (info != null) {
                return true;
            }
        }

        // Otherwise inspect the host to determine what to do
        if (host is IDictionary) {
            IDictionary dictionary = (IDictionary)host;
            return dictionary.Contains(key);
        } else if (host is IList) {
            IList list = (IList)host;
            int i = TypeCoercer.CoerceToType<int>(this, key);
            return i < list.Count;
        } else if (host is Array) {
            Array array = (Array)host;
            int i = TypeCoercer.CoerceToType<int>(this, key);
            return i < array.Length;
        } else {
            return false;
        }
    }
}