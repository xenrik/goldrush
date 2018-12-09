using NUnit.Framework;
using System.Collections.Generic;

public class NullCoalesceParserTest : BinaryParserTest<NullCoalesceParser,NullCoalesceToken> {
    public override string ParserSymbol { get { return "??"; } }
}
