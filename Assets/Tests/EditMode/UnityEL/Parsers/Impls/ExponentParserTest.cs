using NUnit.Framework;
using System.Collections.Generic;

public class ExponentParserTest : BinaryParserTest<ExponentParser, ExponentToken> {
    public override string ParserSymbol { get { return "**"; } }
}
