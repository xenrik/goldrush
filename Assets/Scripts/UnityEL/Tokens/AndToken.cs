﻿public class AndToken : BinaryToken {
    public override string Name { get { return "and"; } }

    public AndToken(int position, RawToken parent) : base(position, parent) {
    }
}