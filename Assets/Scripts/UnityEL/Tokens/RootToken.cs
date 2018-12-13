using UnityEngine;
using UnityEditor;

public class RootToken : TokenImpl {
    public override string Name { get { return "root"; } }

    public RootToken() {
    }

    public RootToken(int position) : base(position) {
    }

    public override void Validate() {
        base.Validate();

        // When we are validated, we must have a single child
        if (Children.Count > 0) {
            throw new ParserException(this, "Invalid expression (too many children)");
        } else if (Children.Count == 0) {
            throw new ParserException(this, "Invalid expression (no children)");
        }
    }
}