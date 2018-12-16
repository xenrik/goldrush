using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

public interface FunctionResolver {
    MethodInfo ResolveFunction(string name, System.Type[] argumentTypes);
}