using UnityEngine;
using UnityEditor;

public class GroupToken : TokenImpl {
    public override string Name { get { return "group"; } }

    public GroupToken() {
    }

    public GroupToken(int position) : base(position) {
    }
}