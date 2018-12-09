using NUnit.Framework;
using System.Collections.Generic;

public class BitwiseOrParserTest : BinaryParserTest<BitwiseOrParser,BitwiseOrToken> {
    public override string ParserSymbol { get { return "|"; } }
}
