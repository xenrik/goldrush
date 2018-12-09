using NUnit.Framework;
using System.Collections.Generic;

public class OrParserTest : BinaryParserTest<OrParser,OrToken> {
    public override string ParserSymbol { get { return "||"; } }
}
