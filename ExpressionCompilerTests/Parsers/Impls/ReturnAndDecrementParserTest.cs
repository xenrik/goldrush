﻿public class ReturnAndDecrementParserTest : LeftHandUnaryParserTest<ReturnAndDecrementParser, ReturnAndDecrementToken> {
    public override string ParserSymbol { get { return "--"; } }
}