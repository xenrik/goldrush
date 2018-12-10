using System.Collections;
using System.Collections.Generic;
using System.Text;

public abstract class RawToken : Token, IEnumerable<RawToken> {
    /** The position this token started at in the expression */
    public int Position { get; private set; }

    /** The name for this token */
    public abstract string Name { get; }

    /** A string to print for this token when debugging */
    public string DebugName { get { return $"<{Name}@{Position}>"; } }

    /**
     * The parent of this token
     */
    public RawToken Parent { get; private set; }

    /**
     * The number of children this token has
     */
    public int ChildCount {
        get {
            if (childTokens == null) {
                return 0;
            } else {
                return childTokens.Count;
            }
        }
    }

    /**
     * Get a child from this token.
     */
    public RawToken this[int index] {
        get {
            if (childTokens == null) {
                throw new System.IndexOutOfRangeException();
            } else {
                return childTokens[index];
            }
        }
    }
    
    /** The children of this token */
    private List<RawToken> childTokens;

    /** Constructor for a root token */
    public RawToken() {
        this.Position = 0;
        this.Parent = null;
    }

    /**
     * Constructor accepting the expression offset position and the parent token
     */
    public RawToken(int position, RawToken parent) {
        this.Position = position;
        this.Parent = parent;

        if (parent.childTokens == null) {
            parent.childTokens = new List<RawToken>();
        }
        parent.childTokens.Add(this);
    }

    

    /**
     * Resolve this raw token into a resolved token
     */
    public virtual void Resolve() {
        // TODO: This will change to abstract once everything is refactored.
        throw new System.NotImplementedException();
    }

    public sealed override string ToString() {
        return DoToString("");
    }

    public override int GetHashCode() {
        const int PRIME = 31;
        int hashCode = 1;

        hashCode = PRIME * hashCode + GetType().GetHashCode();
        hashCode = PRIME * hashCode + Position;

        return hashCode;
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

        RawToken other = (RawToken)obj;
        if (other.Position != Position) {
            return false;
        }

        if (includeChildren && (other.childTokens != null || childTokens != null)) {
            if (other.childTokens == null || childTokens == null) {
                return false;
            }

            if (other.childTokens.Count != childTokens.Count) {
                return false;
            }

            for (int i = 0; i < other.childTokens.Count; ++i) {
                if (!other.childTokens[i].Equals(childTokens[i])) {
                    return false;
                }
            }
        }

        if (Parent != null) {
            return Parent.Equals(other.Parent, false);
        } else {
            return true;
        }
    }

    public IEnumerator<RawToken> GetEnumerator() {
        if (childTokens == null) {
            return System.Linq.Enumerable.Empty<RawToken>().GetEnumerator();
        } else {
            return childTokens.GetEnumerator();
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        if (childTokens == null) {
            return System.Linq.Enumerable.Empty<RawToken>().GetEnumerator();
        } else {
            return childTokens.GetEnumerator();
        }
    }

    protected virtual string GetTokenDataString() {
        return null;
    }

    private string DoToString(string indent) {
        StringBuilder buffer = new StringBuilder();
        buffer.Append($"{indent}{DebugName}");
        string data = GetTokenDataString();
        if (data != null) {
            buffer.Append("[" + data + "]");
        }
        if (childTokens != null) {
            buffer.AppendLine("{");
            bool first = true;
            foreach (RawToken child in childTokens) {
                if (!first) {
                    buffer.AppendLine(",");
                }
                first = false;
                buffer.Append(child.DoToString(indent + "   "));
            }
            if (childTokens.Count > 0) {
                buffer.AppendLine();
            }
            buffer.Append(indent + "}");
        }

        return buffer.ToString();
    }
}