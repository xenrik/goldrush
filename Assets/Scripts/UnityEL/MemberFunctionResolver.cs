using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

public interface MemberFunctionResolver {
    MethodInfo ResolveFunction(System.Type type, string name, System.Type[] argumentTypes);
}