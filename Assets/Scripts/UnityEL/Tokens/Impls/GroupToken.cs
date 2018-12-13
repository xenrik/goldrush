using UnityEngine;
using UnityEditor;

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
}