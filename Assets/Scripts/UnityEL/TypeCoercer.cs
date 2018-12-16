using UnityEngine;
using UnityEditor;
using System;

public sealed class TypeCoercer {
    public static T CoerceToType<T>(TokenImpl context, object value) {
        return (T)DoCoerce(typeof(T), context, value, default(T));
    }

    public static object CoerceToType(System.Type type, TokenImpl context, object value) {
        object defaultValue;
        if (type.IsValueType) { 
            defaultValue = Activator.CreateInstance(type);
        } else {
            defaultValue = null;
        }

        return DoCoerce(type, context, value, defaultValue);
    }

    private static object DoCoerce(Type type, TokenImpl context, object value, object defaultValue) { 
        if (value == null) {
            return defaultValue;
        } else if (value.GetType() == type) {
            return value;
        } else if (type == typeof(int)) {
            if (value.GetType() == typeof(float)) {
                float f = (float)value;
                return (int)f;
            } else if (value.GetType() == typeof(string)) {
                string s = (string)value;
                int result;
                if (int.TryParse(s, out result)) {
                    return result;
                }
            }
        } else if (type == typeof(bool)) {
            if (value.GetType() == typeof(int)) {
                int i = (int)value;
                return i != 0;
            } else if (value.GetType() == typeof(string)) {
                string s = (string)value;
                return s.ToLower().Equals("true");
            }
        } else if (type == typeof(float)) {
            if (value.GetType() == typeof(int)) {
                int i = (int)value;
                return (float)i;
            } else if (value.GetType() == typeof(string)) {
                string s = (string)value;
                float result;
                if (float.TryParse(s, out result)) {
                    return result;
                }
            }
        } else if (type == typeof(string)) {
            return value.ToString();
        } else if (type.IsAssignableFrom(value.GetType())) {
            return value;
        }

        throw new ParserException(context, $"Cannot convert value of type: {value.GetType()} to: {type}");
    }
}