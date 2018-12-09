public class PropertyAccessorToken : RawToken {
    public override string Name { get { return "propertyAccessor"; } }

    public PropertyAccessorToken() {
    }

    public PropertyAccessorToken(int position, RawToken parent) : base(position, parent) {
    }
}