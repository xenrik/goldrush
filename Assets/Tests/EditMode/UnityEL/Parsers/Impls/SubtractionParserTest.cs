using NUnit.Framework;
using System.Collections.Generic;

public class SubtractionParserTest : BinaryParserTest<SubtractionParser,SubtractionToken> {
    public override string ParserSymbol { get { return "-"; } }
}
