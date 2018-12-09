using NUnit.Framework;
using System.Collections.Generic;

public class DivisionParserTest : BinaryParserTest<DivisionParser, DivisionToken> {
    public override string ParserSymbol { get { return "/"; } }
}
