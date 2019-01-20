public class AndParserTest : BinaryParserTest<AndParser, AndToken> {
    public override string ParserSymbol { get { return "&&"; } }
}