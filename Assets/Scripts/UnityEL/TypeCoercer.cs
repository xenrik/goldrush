using UnityEngine;
using UnityEditor;

public sealed class TypeCoercer {
    public static T CoerceToType<T>(TokenImpl context, object value) {
        if (value == null) {
            return default(T);
        } else if (value.GetType() == typeof(T)) {
            return (T)value;
        } else if (typeof(T) == typeof(int)) {
            if (value.GetType() == typeof(float)) {
                int i = (int)(float)value;
                return (T)(object)i;
            } else if (value.GetType() == typeof(string)) {
                string s = (string)value;
                int result;
                if (int.TryParse(s, out result)) {
                    return (T)(object)result;
                }
            }
        } else if (typeof(T) == typeof(bool)) {
            if (value.GetType() == typeof(int)) {
                bool b = ((int)value) != 0;
                return (T)(object)b;
            } else if (value.GetType() == typeof(string)) {
                string s = (string)value;
                return (T)(object)s.ToLower().Equals("true");
            }
        } else if (typeof(T) == typeof(float)) {
            if (value.GetType() == typeof(int)) {
                float f = (float)(int)value;
                return (T)(object)f;
            } else if (value.GetType() == typeof(string)) {
                string s = (string)value;
                float result;
                if (float.TryParse(s, out result)) {
                    return (T)(object)result;
                }
            }
        } else if (typeof(T) == typeof(string)) {
            return (T)(object)value.ToString();
        } else if (typeof(T).IsAssignableFrom(value.GetType())) {
            return (T)value;
        }

        throw new ParserException(context, $"Cannot convert value of type: {value.GetType()} to: {typeof(T)}");
    }
}