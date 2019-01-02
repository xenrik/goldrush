using System;
using System.Reflection;

/**
 * A class that can be used to make public static methods on the given type available through the FunctionResolver interface.
 * The class can be extended and the Default Constructor used to provide the public static methods of the subclass
 */
public class StaticMethodsFunctionResolver : FunctionResolver {

    /** The type to provide the methods for */
    private Type type;

    /**
     * Default constructor will provide the public static methods available on this instance.
     */
    protected StaticMethodsFunctionResolver() : this(null) {
    }

    /**
     * Constructor accepting the type to provide the methods for
     */
    public StaticMethodsFunctionResolver(Type type) {
        if (type == null) {
            type = GetType();
        }

        this.type = type;
    }

    public MethodInfo ResolveFunction(string name, Type[] parameterTypes) {
        foreach (MethodInfo methodInfo in type.GetMethods()) {
            if (!methodInfo.IsStatic) {
                continue;
            }

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