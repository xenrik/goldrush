using System.Reflection;

public interface MemberFunctionResolver {
    MethodInfo ResolveFunction(System.Type type, string name, System.Type[] argumentTypes);
}