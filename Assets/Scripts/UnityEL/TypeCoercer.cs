using UnityEngine;
using UnityEditor;
using System;

/**
 * The TypeCoercer is used to adapt between different representations of a given value. For example it can convert a string
 * to an int, or vice-versa.
 * 
 * It can also report on the 'similarity' between two types, where a higher number indicates they are less similar (int.MaxValue
 * is returned if the types cannot be coerced). Similarity is based wether two types are fundamentally the same kind of data,
 * for example two types of number (int and float) are more similar than an int and a string. Note that an int -> float conversion
 * is more similar than a float->int conversion (as no information can be lost). Zero is returned if the source and target type
 * are the same. 
 */
public sealed class TypeCoercer {
    public static int GetTypeSimilarity(Type sourceType, Type targetType) {
        if (sourceType == targetType) {
            return 0;
        } else if (sourceType == typeof(int)) {
            if (targetType == typeof(float)) {
                return 10;
            } else if (targetType == typeof(string)) {
                return 20;
            } else if (targetType == typeof(bool)) {
                return 30;
            }
        } else if (sourceType == typeof(float)) {
            if (targetType == typeof(int)) {
                return 15;
            } else if (targetType == typeof(string)) {
                return 20;
            } else if (targetType == typeof(bool)) {
                return 30;
            }
        } else if (sourceType == typeof(bool)) {
            if (targetType == typeof(int) ||
                    targetType == typeof(float) ||
                    targetType == typeof(string)) {
                return 20;
            }
        } else if (sourceType == typeof(string)) {
            if (targetType == typeof(int) ||
                    targetType == typeof(float) ||
                    targetType == typeof(bool)) {
                return 30;
            }
        } else if (targetType.IsAssignableFrom(sourceType)) {
            return 30;
        }

        // Return maxValue if we get here
        return int.MaxValue;
    }

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
        } else if (type == typeof(object) || value.GetType() == type) {
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
            } else if (value.GetType() == typeof(bool)) {
                bool b = (bool)value;
                return b ? 1 : 0;
            }
        } else if (type == typeof(bool)) {
            if (value.GetType() == typeof(int)) {
                int i = (int)value;
                return i != 0;
            } else if (value.GetType() == typeof(float)) {
                float f = (float)value;
                return f != 0;
            } else if (value.GetType() == typeof(string)) {
                string s = (string)value;
                return s.Equals("true", StringComparison.InvariantCultureIgnoreCase);
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
            } else if (value.GetType() == typeof(bool)) {
                bool b = (bool)value;
                return b ? 1.0f : 0.0f;
            }
        } else if (type == typeof(string)) {
            if (value.GetType() == typeof(bool)) {
                // Force to lowercase for consistency
                return value.ToString().ToLowerInvariant();
            } else {
                return value.ToString();
            }
        } else if (type.IsAssignableFrom(value.GetType())) {
            return value;
        }

        throw new ParserException(context, $"Cannot convert value of type: {value.GetType()} to: {type}");
    }
}