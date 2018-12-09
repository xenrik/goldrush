public class GroupOrFunctionToken : RawToken {
    public override string Name { get { return "groupOrFunction"; } }

    public GroupOrFunctionToken() {
    }

    public GroupOrFunctionToken(int position, RawToken parent) : base(position, parent) {
    }
}