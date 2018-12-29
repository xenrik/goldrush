using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Text;

public class ConditionalOperatorToken : TokenImpl {
    public override string Name { get { return "conditionalOperator"; } }

    public TokenImpl Test { get; private set; }
    public TokenImpl ResultIfTrue { get; private set; }
    public TokenImpl ResultIfFalse { get; private set; }

    public ConditionalOperatorToken() {
    }
    public ConditionalOperatorToken(int position, TokenImpl test, TokenImpl resultIfTrue, TokenImpl resultIfFalse) : base(position) {
        this.Test = test;
        this.ResultIfTrue = resultIfTrue;
        this.ResultIfFalse = resultIfFalse;
    }
    
    public override int GetHashCode() {
        const int PRIME = 37;
        int hashCode = base.GetHashCode();
        hashCode = PRIME * hashCode + (Test != null ? Test.GetHashCode() : 0);
        hashCode = PRIME * hashCode + (ResultIfTrue != null ? ResultIfTrue.GetHashCode() : 0);
        hashCode = PRIME * hashCode + (ResultIfFalse != null ? ResultIfFalse.GetHashCode() : 0);

        return hashCode;
    }

    public override bool Equals(object obj, bool includeChildren) {
        if (!base.Equals(obj, includeChildren)) {
            return false;
        }

        ConditionalOperatorToken other = (ConditionalOperatorToken)obj;

        if (Test != null) {
            if (!Test.Equals(other.Test)) {
                return false;
            }
        } else if (other.Test != null) {
            return false;
        }

        if (ResultIfTrue != null) {
            if (!ResultIfTrue.Equals(other.ResultIfTrue)) {
                return false;
            }
        } else if (other.ResultIfTrue != null) {
            return false;
        }

        if (ResultIfFalse != null) {
            if (!ResultIfFalse.Equals(other.ResultIfFalse)) {
                return false;
            }
        } else if (other.ResultIfFalse != null) {
            return false;
        }

        return true;
    }

    protected override string GetTokenDataString() {
        StringBuilder buffer = new StringBuilder();
        buffer.AppendLine();
        buffer.AppendLine($"Test={(Test != null ? Test.ToString() : "null")},");
        buffer.AppendLine($"ResultIfTrue={(ResultIfTrue!= null ? ResultIfTrue.ToString() : "null")}");
        buffer.AppendLine($"ResultIfFalse={(ResultIfFalse != null ? ResultIfFalse.ToString() : "null")}");

        return buffer.ToString();
    }

    public override object Evaluate(UnityELEvaluator context) {
        object testResult = Test.Evaluate(context);
        bool testBool = TypeCoercer.CoerceToType<bool>(this, testResult);
        if (testBool) {
            return ResultIfTrue.Evaluate(context);
        } else {
            return ResultIfFalse.Evaluate(context);
        }
    }
}