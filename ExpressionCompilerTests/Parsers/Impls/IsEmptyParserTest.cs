public class IsEmptyParserTest : UnaryParserTest<IsEmptyParser, IsEmptyToken> {
    public override string ParserSymbol { get { return "empty"; } }
    public override bool SymbolRequiresTrailingSpace { get { return true; } }
}