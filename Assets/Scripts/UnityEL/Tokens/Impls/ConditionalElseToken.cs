using UnityEngine;
using UnityEditor;

public class ConditionalElseToken : RawToken {
    public override string Name { get { return "conditionalElse"; } }

    public ConditionalElseToken() {
    }

    public ConditionalElseToken(int position, RawToken parent) : base(position, parent) {
    }
}