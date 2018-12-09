using System.Collections.Generic;

public class ComplementToken : UnaryToken {
    public override string Name { get { return "complement"; } }

    public ComplementToken() : base() {
    }
    public ComplementToken(int position, RawToken parent) : base(position, parent) {
    }
}