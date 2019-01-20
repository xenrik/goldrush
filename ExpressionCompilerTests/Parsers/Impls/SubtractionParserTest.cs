public class SubtractionParserTest : BinaryParserTest<SubtractionParser, SubtractionToken> {
    public override string ParserSymbol { get { return "-"; } }

    public override bool IsLenient { get { return true; } }
}