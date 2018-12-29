using UnityEngine;
using UnityEditor;
using System;

public class IsToken : BinaryToken {
    public override string Name { get { return "is"; } }

    public IsToken() {
    }

    public IsToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        if (Rhs is NullToken) {
            // Special case...
            if (lhsResult == null) {
                return true;
            } else {
                return false;
            }
        }

        return DoEvaluate(context, lhsResult);
    }

    protected bool DoEvaluate(UnityELEvaluator context, object lhsResult) {
        Type lhsType = lhsResult?.GetType();

        // Otherwise the rhs must identify the type to match
        object rhsResult = Rhs.Evaluate(context);
        if (rhsResult == null) {
            return false;
        }
        string rhsString = TypeCoercer.CoerceToType<string>(this, rhsResult);

        // Special handling for some primitives
        if (rhsString.Equals("int", StringComparison.InvariantCultureIgnoreCase) || 
                rhsString.Equals("integer", StringComparison.InvariantCultureIgnoreCase)) {
            // Is it an int?
            if (typeof(int).Equals(lhsType)) {
                return true;
            } else if (typeof(float).Equals(lhsType)) {
                // We use floats internally, so we allow them to be considered as 'ints' if it would
                // not result in the loss of any data
                float floatValue = (float)lhsResult;
                int intValue = (int)floatValue;

                return intValue == floatValue;
            } else {
                return false;
            }
        } else if (rhsString.Equals("float", StringComparison.InvariantCultureIgnoreCase)) {
            // Allow floats or ints?
            return typeof(float).Equals(lhsType) || typeof(int).Equals(lhsType);
        } else if (rhsString.Equals("bool", StringComparison.InvariantCultureIgnoreCase) || 
                rhsString.Equals("boolean", StringComparison.InvariantCultureIgnoreCase)) {
            return typeof(bool).Equals(lhsType);
        } else if (lhsType != null) {
            // Otherwise we must match the end of the type name
            return lhsType.FullName.EndsWith(rhsString, StringComparison.InvariantCultureIgnoreCase);
        } else {
            return false;
        }
    }
}