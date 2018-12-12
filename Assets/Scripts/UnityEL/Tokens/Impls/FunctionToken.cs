using UnityEngine;
using UnityEditor;

public class FunctionToken : TokenImpl {
    public override string Name { get { return "function"; } }

    public FunctionToken() {
    }

    public FunctionToken(int position) : base(position) {
    }
}