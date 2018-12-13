using System.Collections;
using System.Collections.Generic;
using System.Text;

public abstract class TokenImpl : Token {
    /** The position this token started at in the expression */
    public int Position { get; private set; }

    /** The name for this token */
    public abstract string Name { get; }

    /** A string to print for this token when debugging */
    public string DebugName { get { return $"<{Name}@{Position}>"; } }

    /** The children of this token */
    public List<TokenImpl> Children { get; private set; }

    /** Constructor for a root token */
    public TokenImpl() : this(0) {
    }

    /**
     * Constructor accepting the expression offset position and the parent token
     */
    public TokenImpl(int position) {
        this.Position = position;
        this.Children = new List<TokenImpl>();
    }

    /**
     * Convenience method to add a child. 
     */
    public void AddChild(TokenImpl token) {
        Children.Add(token);
    }

    /**
     * Convenience method to remove the last child and return it
     */
    public TokenImpl PopChild() {
        if (Children.Count == 0) {
            throw new System.InvalidOperationException("No children on this token");
        }

        TokenImpl child = Children[Children.Count - 1];
        Children.RemoveAt(Children.Count - 1);

        return child;
    }

    /**
     * Convenience method to get the last child without removing it.
     * 
     * Returns null if there are no children.
     */
    public TokenImpl PeekChild() {
        if (Children.Count == 0) {
            return null;
        }

        return Children[Children.Count - 1];
    }

    public override int GetHashCode() {
        const int PRIME = 31;
        int hashCode = 1;

        hashCode = PRIME * hashCode + GetType().GetHashCode();
        hashCode = PRIME * hashCode + Position;

        return hashCode;
    }

    public sealed override string ToString() {
        return DoToString("");
    }

    private string DoToString(string indent) {
        StringBuilder buffer = new StringBuilder();
        buffer.Append($"{indent}{DebugName}");
        string data = GetTokenDataString();
        if (data != null) {
            buffer.Append("[" + data + "]");
        }
        if (Children.Count > 0) {
            buffer.AppendLine("{");
            bool first = true;
            foreach (TokenImpl child in Children) {
                if (!first) {
                    buffer.AppendLine(",");
                }
                first = false;
                buffer.Append(child.DoToString(indent + "   "));
            }
            buffer.AppendLine();
            buffer.Append(indent + "}");
        }

        return buffer.ToString();
    }

    public sealed override bool Equals(object obj) {
        return Equals(obj, true);
    }

    public virtual bool Equals(object obj, bool includeChildren) {
        if (obj == null) {
            return false;
        }
        if (obj.GetType() != GetType()) {
            return false;
        }

        TokenImpl other = (TokenImpl)obj;
        if (other.Position != Position) {
            return false;
        }

        if (includeChildren) {
            if (other.Children.Count != Children.Count) {
                return false;
            }

            for (int i = 0; i < other.Children.Count; ++i) {
                if (!other.Children[i].Equals(Children[i])) {
                    return false;
                }
            }
        }

        return true;
    }

    protected virtual string GetTokenDataString() {
        return null;
    }
}