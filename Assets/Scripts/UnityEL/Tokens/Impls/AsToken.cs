using UnityEngine;
using UnityEditor;
using System;

public class AsToken : BinaryToken {
    public override string Name { get { return "as"; } }

    public AsToken() {
    }

    public AsToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        string rhsTypeName = TypeCoercer.CoerceToType<string>(this, rhsResult);
        if (rhsTypeName == null) {
            throw new ParserException("Right-hand side of 'as' evaluated to null");
        }

        if ("int".Equals(rhsTypeName)) {
            return TypeCoercer.CoerceToType<int>(this, lhsResult);
        } else if ("float".Equals(rhsTypeName)) {
            return TypeCoercer.CoerceToType<float>(this, lhsResult);
        } else if ("string".Equals(rhsTypeName)) {
            return TypeCoercer.CoerceToType<string>(this, lhsResult);
        } else if ("bool".Equals(rhsTypeName)) {
            return TypeCoercer.CoerceToType<bool>(this, lhsResult);
        } else {
            Type type = Type.GetType(rhsTypeName);
            if (type == null) {
                throw new ParserException($"Could not resolve type from string: {rhsTypeName}");
            }

            return TypeCoercer.CoerceToType(type, this, lhsResult);
        }
    }
}