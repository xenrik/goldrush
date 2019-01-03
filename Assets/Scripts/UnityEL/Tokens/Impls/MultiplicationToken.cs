using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

public class MultiplicationToken : BinaryToken {
    public override string Name { get { return "multiplication"; } }

    public MultiplicationToken() {
    }

    public MultiplicationToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object lhsResult = Lhs.Evaluate(context);
        object rhsResult = Rhs.Evaluate(context);

        // See if the lhs supports an operator overload for the rhs 
        if (lhsResult != null) {
            OperatorInfo info = FindOperator(lhsResult.GetType(), "op_Multiply", rhsResult);
            if (info != null) {
                object coercedRhs = TypeCoercer.CoerceToType(info.OperandType, this, rhsResult);
                return info.Method.Invoke(null, new object[] { lhsResult, coercedRhs });
            }
        }

        // Otherwise just use a float multiplication
        float lhsFloat = TypeCoercer.CoerceToType<float>(this, lhsResult);
        float rhsFloat = TypeCoercer.CoerceToType<float>(this, rhsResult);

        return lhsFloat * rhsFloat;
    }

    private OperatorInfo FindOperator(Type type, string operatorName, object operand) {
        OperatorInfo bestMethod = null;
        int bestSimilarity = int.MaxValue;
        foreach (MethodInfo methodInfo in type.GetMethods()) {
            if (!methodInfo.IsStatic) {
                continue;
            } else if (!methodInfo.Name.Equals(operatorName)) {
                continue;
            }

            ParameterInfo[] methodParameters = methodInfo.GetParameters();
            if (methodParameters.Length != 2) {
                continue;
            }

            Type operandType = methodParameters[1].ParameterType;
            if (operand == null || operandType.Equals(operand.GetType())) {
                return new OperatorInfo(methodInfo, operandType);
            }

            int similarity = TypeCoercer.GetTypeSimilarity(operandType, operand.GetType());
            if (similarity < bestSimilarity) {
                bestMethod = new OperatorInfo(methodInfo, operandType);
            }
        }

        return bestMethod;
    }

    private class OperatorInfo {
        public MethodInfo Method;
        public Type OperandType;

        public OperatorInfo(MethodInfo method, Type operandType) {
            this.Method = method;
            this.OperandType = operandType;
        }
    }
}