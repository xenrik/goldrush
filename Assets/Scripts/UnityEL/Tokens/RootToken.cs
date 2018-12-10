using UnityEngine;
using UnityEditor;

public class RootToken : TokenImpl {
    public override string Name { get { return "root"; } }

    public RootToken() {
    }

    public RootToken(int position) : base(position) {
    }

    public object Resolve() {
        // To resolve the root token, we must have a single child which is
        // a value token
        if (Children.Count != 0) {
            throw new ParserException(this, "Unable to resolve root token");
        } else if (!(Children[0] is ValueToken)) {
            throw new ParserException(this, "Expression did not resolve to a value");
        }

        ValueToken valueToken = (ValueToken)Children[0];
        return valueToken.Value;
    }
}