﻿public class BitwiseAndToken : BinaryToken {
    public override string Name { get { return "bitwiseAnd"; } }

    public BitwiseAndToken(int position, RawToken parent) : base(position, parent) {
    }
}