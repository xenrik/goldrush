using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System;

public class FunctionToken : TokenImpl, CloseableToken, ExistsSupport {
    public override string Name { get { return "function"; } }
    public TokenImpl FunctionName { get; private set; }
    public bool IsClosed { get; private set; }

    public FunctionToken() {
    }

    public FunctionToken(int position, TokenImpl functionName) : base(position) {
        this.FunctionName = functionName;
    }

    public override int GetHashCode() {
        const int PRIME = 37;
        if (FunctionName != null) {
            return base.GetHashCode() * PRIME + FunctionName.GetHashCode();
        } else {
            return base.GetHashCode();
        }
    }

    public override bool Equals(object other, bool includeChildren) {
        if (!base.Equals(other, includeChildren)) {
            return false;
        }

        FunctionToken otherToken = (FunctionToken)other;
        if (FunctionName != null) {
            return FunctionName.Equals(otherToken.FunctionName);
        } else {
            return otherToken.FunctionName == null;
        }
    }

    protected override string GetTokenDataString() {
        return FunctionName == null ? "null" : FunctionName.ToString();
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

        bool validFunctionToken = false;
        validFunctionToken |= FunctionName is PropertyAccessToken;
        validFunctionToken |= FunctionName is IdentifierToken;

        if (!validFunctionToken) {
            throw new ParserException(this, $"Unsupport token type for function name: {FunctionName}");
        }
    }

    public bool Exists(UnityELEvaluator context) {
        FunctionDetails functionDetails = ResolveFunction(context);
        return functionDetails.Function != null;
    }


    public override object Evaluate(UnityELEvaluator context) {
        FunctionDetails functionDetails = ResolveFunction(context);
        if (functionDetails.Function == null) {
            if (functionDetails.ResolutionFailedReason == null) {
                functionDetails.ResolutionFailedReason = $"Unable to resolve function: {functionDetails.Name} " +
                    $"(host={functionDetails.Host})"; //, namespace={functionDetails.Namespace})";
            }
            throw new NoSuchFunctionException(this, functionDetails.ResolutionFailedReason);
        }

        // Map parameters...
        MethodInfo function = functionDetails.Function;
        List<object> parameters = functionDetails.Parameters;
        ParameterInfo[] parameterInfos = function.GetParameters();
        if (parameterInfos.Length > 0) {
            // If there is a different number of arguments, see if the last argument is a params
            // and then wrap up the required number of arguments in an array (coercing as needed)
            ParameterInfo lastParam = parameterInfos[parameterInfos.Length - 1];
            if (lastParam.IsDefined(typeof(ParamArrayAttribute), false)) {
                int varargCount = parameters.Count - parameterInfos.Length + 1;
                Type elementType = lastParam.ParameterType.GetElementType();
                Array varargs = Array.CreateInstance(elementType, varargCount);
                for (int i = 0; i < varargCount; ++i) {
                    varargs.SetValue(TypeCoercer.CoerceToType(elementType, this,
                        parameters[parameters.Count - varargCount + i]), i);
                }

                parameters[parameterInfos.Length - 1] = varargs;
                parameters.RemoveRange(parameterInfos.Length,
                    parameters.Count - parameterInfos.Length);
            }

            // Coerce the parameters if needed
            for (int i = 0; i < parameters.Count; ++i) {
                parameters[i] = TypeCoercer.CoerceToType(parameterInfos[i].ParameterType,
                    this, parameters[i]);
            }
        }

        return function.Invoke(functionDetails.Host, parameters.ToArray());
    }

    private FunctionDetails ResolveFunction(UnityELEvaluator context) {
        FunctionDetails functionDetails = new FunctionDetails();

        // See if there is a host object, otherwise we're looking for a static function on the evaluator
        if (FunctionName is PropertyAccessToken) {
            PropertyAccessToken propertyAccess = (PropertyAccessToken)FunctionName;
            functionDetails.Host = propertyAccess.Host.Evaluate(context);
            if (functionDetails.Host == null) {
                return null;
            }

            functionDetails.Name = propertyAccess.Property.Value;
        } else if (FunctionName is IdentifierToken) {
            IdentifierToken identifier = (IdentifierToken)FunctionName;
            functionDetails.Host = null;
            functionDetails.Name = identifier.Value;
        }

        // Resolve arguments (if any)
        functionDetails.Parameters = new List<object>();
        List<System.Type> types = new List<System.Type>();
        foreach (TokenImpl childToken in Children) {
            object value = childToken.Evaluate(context);
            functionDetails.Parameters.Add(value);

            if (value != null) {
                types.Add(value.GetType());
            } else {
                types.Add(null);
            }
        }

        if (functionDetails.Host != null) {
            if (context.MemberFunctionResolver == null) {
                functionDetails.ResolutionFailedReason = $"Cannot resolve a member function as no MemberFunctionResolver has been configured on the context";
            } else {
                functionDetails.Function = context.MemberFunctionResolver.ResolveFunction(functionDetails.Host.GetType(), functionDetails.Name, types.ToArray());
            }
        /*} else if (functionDetails.Namespace != null) {
            if (!context.FunctionResolvers.ContainsKey(functionDetails.Namespace)) {
                functionDetails.ResolutionFailedReason = $"Unknown function namespace: {functionDetails.Namespace}";
            } else {
                functionDetails.Function = context.FunctionResolvers[functionDetails.Namespace].ResolveFunction(functionDetails.Name, types.ToArray());
            }*/
        } else {
            if (context.DefaultFunctionResolver == null) {
                functionDetails.ResolutionFailedReason = $"No function namespace was supplied and no default function resolver was setup";
            } else {
                functionDetails.Function = context.DefaultFunctionResolver.ResolveFunction(functionDetails.Name, types.ToArray());
            }
        }

        return functionDetails;
    }

    private class FunctionDetails {
        public string Name;
        //public string Namespace;
        public object Host;

        public string ResolutionFailedReason;

        public MethodInfo Function;
        public List<object> Parameters;
    }
}