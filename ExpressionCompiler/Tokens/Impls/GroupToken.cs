public class GroupToken : TokenImpl, CloseableToken {
    public override string Name { get { return "group"; } }
    public bool IsClosed { get; private set; }

    public GroupToken() {
    }

    public GroupToken(int position) : base(position) {
    }

    public void Close() {
        if (IsClosed) {
            throw new ParserException(this, "Group has already been closed");
        }

        IsClosed = true;
    }

    public override void Validate() {
        if (!IsClosed) {
            throw new ParserException(this, "Has not been closed");
        }

        // Should have a single child
        if (Children.Count > 1) {
            throw new ParserException(this, "Invalid expression (too many children)");
        } else if (Children.Count == 0) {
            throw new ParserException(this, "Invalid expression (no children)");
        }
    }

    public override object Evaluate(UnityELEvaluator context) {
        TokenImpl child = Children[0];
        return child.Evaluate(context);
    }
}