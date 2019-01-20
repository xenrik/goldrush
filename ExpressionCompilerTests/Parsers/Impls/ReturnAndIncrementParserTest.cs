public class ReturnAndIncrementParserTest : LeftHandUnaryParserTest<ReturnAndIncrementParser, ReturnAndIncrementToken> {
    public override string ParserSymbol { get { return "++"; } }
}