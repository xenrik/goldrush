using System.Collections;

public class IsEmptyToken : UnaryToken {
    public override string Name { get { return "empty"; } }

    public IsEmptyToken() {
    }

    public IsEmptyToken(int position, TokenImpl rhs) : base(position, rhs) {
    }

    public override object Evaluate(UnityELEvaluator context) {
        object rhsResult = Rhs.Evaluate(context);
        if (rhsResult is string) {
            string s = (string)rhsResult;
            return s.Length == 0;
        } else if (rhsResult is ICollection) {
            ICollection collection = (ICollection)rhsResult;
            return collection.Count == 0;
        } else if (rhsResult is IEnumerable) {
            IEnumerable enumer = (IEnumerable)rhsResult;
            return !enumer.GetEnumerator().MoveNext();
        } else {
            return rhsResult == null;
        }
    }
}