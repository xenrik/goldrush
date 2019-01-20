public class IncrementAndReturnParserTest : UnaryParserTest<IncrementAndReturnParser, IncrementAndReturnToken> {
    public override string ParserSymbol { get { return "++"; } }
}