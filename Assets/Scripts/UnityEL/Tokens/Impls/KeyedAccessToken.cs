using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Collections;

public class KeyedAccessToken : TokenImpl, CloseableToken, ExistsSupport, AssignableToken, HostSupport {
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

    public void Assign(UnityELEvaluator context, object value) {
        object host = Host.Evaluate(context);
        if (host == null) {
            throw new ParserException(this, $"Did not resolve host object: {Host}");
        }
        System.Type hostType = host.GetType();

        object key = Children[0].Evaluate(context);
        System.Type keyType = key?.GetType();

        // If the key is a string, we need to see if there is a property on the host that matches
        if (key is string) {
            PropertyInfo info = hostType.GetProperty((string)key);
            if (info != null) {
                if (!info.CanWrite || info.SetMethod == null || info.SetMethod.IsPrivate) {
                    throw new ParserException(this, $"Property: {key} on type: {hostType} is read only");
                }

                System.Type propertyType = info.PropertyType;
                object coercedValue = TypeCoercer.CoerceToType(propertyType, this, value);

                info.SetValue(host, coercedValue);
                return;
            }
        }

        // Otherwise inspect the host to determine what to do
        if (host is IDictionary) {
            // See if there is a generic type information
            Type genericDictionaryType = typeof(IDictionary<,>);
            MethodInfo assignGenericDictionaryMethod = this.GetType().GetMethod("AssignGenericDictionary",
                    BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (Type type in hostType.GetInterfaces()) {
                if (type.IsGenericType &&
                        type.GetGenericTypeDefinition() == genericDictionaryType) {
                    assignGenericDictionaryMethod.MakeGenericMethod(type.GetGenericArguments())
                        .Invoke(this, new object[] { host, key, value });
                    return;
                }
            }

            // Otheriwse, just use IDictionary
            IDictionary dictionary = (IDictionary)host;
            dictionary[key] = value;
        } else if (host is IList) {
            int i = TypeCoercer.CoerceToType<int>(this, key);

            // See if there is a generic type information available
            Type genericListType = typeof(IList<>);
            MethodInfo assignGenericListMethod = this.GetType().GetMethod("AssignGenericList",
                    BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (Type type in hostType.GetInterfaces()) {
                if (type.IsGenericType &&
                        type.GetGenericTypeDefinition() == genericListType) {
                    assignGenericListMethod.MakeGenericMethod(type.GetGenericArguments())
                        .Invoke(this, new object[] { host, i, value });
                    return;
                }
            }

            // Otherwise just use IList

            // Expand the list if needed
            IList list = (IList)host;
            while (i >= list.Count) {
                list.Add(null);
            }

            list[i] = value;
        } else if (host is Array) {
            Array array = (Array)host;
            int i = TypeCoercer.CoerceToType<int>(this, key);
            if (i >= array.Length) {
                throw new ParserException(this, $"Array index out of bounds: {i}, length: {array.Length}");
            }
            array.SetValue(value, i);
        } else {
            throw new ParserException(this, $"Unsupported host value type: {hostType}, or unknown property: {key}");
        }
    }

    private void AssignGenericDictionary<TKey,TValue>(IDictionary<TKey,TValue> dictionary, object key, object value) {
        TKey coercedKey = TypeCoercer.CoerceToType<TKey>(this, key);
        TValue coercedValue = TypeCoercer.CoerceToType<TValue>(this, value);

        dictionary[coercedKey] = coercedValue;
    }

    private void AssignGenericList<TValue>(IList<TValue> list, int index, object value) {
        // Expand the list if needed
        while (index >= list.Count) {
            list.Add(default(TValue));
        }

        TValue coercedValue = TypeCoercer.CoerceToType<TValue>(this, value);
        list[index] = coercedValue;
    }
}