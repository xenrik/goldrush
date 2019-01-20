public class NotParserTest : UnaryParserTest<NotParser, NotToken> {
    public override string ParserSymbol { get { return "!"; } }
}