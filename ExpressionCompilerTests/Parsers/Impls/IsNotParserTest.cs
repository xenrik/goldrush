public class IsNotParserTest : BinaryParserTest<IsNotParser, IsNotToken> {
    public override string ParserSymbol { get { return "is not"; } }
    public override bool SymbolRequiresTrailingSpace { get { return true; } }
}