public class XorParserTest : BinaryParserTest<XorParser, XorToken> {
    public override string ParserSymbol { get { return "^"; } }
}