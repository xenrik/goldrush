using System;
using System.Reflection;

public class DefaultMemberFunctionResolver : MemberFunctionResolver {
    public MethodInfo ResolveFunction(Type type, string name, Type[] parameterTypes) {
        foreach (MethodInfo methodInfo in type.GetMethods()) {
            if (methodInfo.Name.Equals(name)) {
                ParameterInfo[] methodParameters = methodInfo.GetParameters();
                if (parameterTypes.Length != methodParameters.Length) {
                    continue;
                }

                bool matched = true;
                for (int i = 0; i < parameterTypes.Length; ++i) {
                    if (parameterTypes[i] == null) {
                        continue;
                    }

                    Type methodParameterType = methodParameters[i].ParameterType;
                    if (!parameterTypes[i].Equals(methodParameterType)) {
                        matched = false;
                        break;
                    }
                }

                if (matched) {
                    return methodInfo;
                }
            }
        }

        return null;
    }
}