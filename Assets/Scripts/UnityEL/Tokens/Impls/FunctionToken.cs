using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

public class FunctionToken : TokenImpl, CloseableToken {
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

    public override object Evaluate(UnityELEvaluator context) {
        // See if there is a host object, otherwise we're looking for a static function on the evaluator
        object host = null;
        string functionName = null;
        string functionNamespace = null;
        if (FunctionName is PropertyAccessToken) {
            PropertyAccessToken propertyAccess = (PropertyAccessToken)FunctionName;
            host = propertyAccess.Host.Evaluate(context);
            if (host == null) {
                return null;
            }

            functionName = propertyAccess.Property.Value;
        } else if (FunctionName is IdentifierToken) {
            IdentifierToken identifier = (IdentifierToken)FunctionName;
            host = null;
            functionName = identifier.Value;
        }

        // Resolve arguments (if any)
        List<object> parameters = new List<object>();
        List<System.Type> types = new List<System.Type>();
        foreach (TokenImpl childToken in Children) {
            object value = childToken.Evaluate(context);
            parameters.Add(value);

            if (value != null) {
                types.Add(value.GetType());
            } else {
                types.Add(null);
            }
        }

        MethodInfo function;
        if (host != null) {
            if (context.MemberFunctionResolver == null) {
                throw new ParserException($"Cannot resolve a member function as no MemberFunctionResolver has been configured on the context");
            }

            function = context.MemberFunctionResolver.ResolveFunction(host.GetType(), functionName, types.ToArray());
        } else if (functionNamespace != null) { 
            if (!context.FunctionResolvers.ContainsKey(functionNamespace)) {
                throw new ParserException($"Unknown function namespace: {functionNamespace}");
            }

            function = context.FunctionResolvers[functionNamespace].ResolveFunction(functionName, types.ToArray());
        } else {
            if (context.DefaultFunctionResolver == null) {
                throw new ParserException($"No function namespace was supplied and no default function resolver was setup");
            }

            function = context.DefaultFunctionResolver.ResolveFunction(functionName, types.ToArray());
        }

        if (function == null) {
            throw new ParserException(this, $"Unable to resolve function: {functionName} (host={host}, namespace={functionNamespace})");
        }

        return function.Invoke(host, parameters.ToArray());
    }
}