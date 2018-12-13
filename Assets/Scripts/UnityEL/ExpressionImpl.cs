using UnityEngine;
using UnityEditor;

public class ExpressionImpl<T> : UnityELExpression<T> {
    private RootToken root;

    public ExpressionImpl(RootToken root) {
        this.root = root;
    }

    public T Evaluate(UnityELEvaluator context) {
        object result = root.Evaluate(context);
        if (result == null) {
            return default(T);
        } else if (result.GetType() == typeof(T)) {
            return (T)result;
        } else {
            return (T)CoerceToType(result);
        }
    }

    private object CoerceToType(object value) {
        if (typeof(T) == typeof(int)) {
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
        } else if (typeof(T) == typeof(bool)) {
            if (value.GetType() == typeof(int)) {
                int i = (int)value;
                return i != 0;
            } else if (value.GetType() == typeof(string)) {
                string s = (string)value;
                return s.ToLower().Equals("true");
            }
        } else if (typeof(T) == typeof(float)) {
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
        } else if (typeof(T) == typeof(string)) {
            return value.ToString();
        } else if (typeof(T).IsAssignableFrom(value.GetType())) {
            return (T)value;
        }

        throw new ParserException(0, $"Cannot convert value of type: {value.GetType()} to: {typeof(T)}");
    }
}