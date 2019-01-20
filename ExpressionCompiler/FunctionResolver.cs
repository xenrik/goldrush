using System.Reflection;

public interface FunctionResolver {
    MethodInfo ResolveFunction(string name, System.Type[] argumentTypes);
}