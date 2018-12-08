public class KeyedAccessorToken : RawToken {
    public override string Name { get { return "keyedAccessor"; } }

    public KeyedAccessorToken() {
    }

    public KeyedAccessorToken(int position, RawToken parent) : base(position, parent) {
    }
}