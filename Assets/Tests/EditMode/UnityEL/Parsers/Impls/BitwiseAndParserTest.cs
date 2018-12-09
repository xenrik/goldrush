using NUnit.Framework;
using System.Collections.Generic;

public class BitwiseAndParserTest : BinaryParserTest<BitwiseAndParser,BitwiseAndToken> {
    public override string ParserSymbol { get { return "&"; } }    
}
