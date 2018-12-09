using NUnit.Framework;
using System.Collections.Generic;

public class XorParserTest : BinaryParserTest<XorParser,XorToken> {
    public override string ParserSymbol { get { return "^"; } }
}
