public class NotExistsParserTest : UnaryParserTest<NotExistsParser, NotExistsToken> {
    public override string ParserSymbol { get { return "not exists"; } }
    public override bool SymbolRequiresTrailingSpace { get { return true; } }
}