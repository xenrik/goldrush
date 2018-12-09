using NUnit.Framework;
using System.Collections.Generic;

public class ModulusParserTest : BinaryParserTest<ModulusParser,ModulusToken> {
    public override string ParserSymbol { get { return "%"; } }
}
