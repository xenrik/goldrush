public class AssignParserTest : BinaryParserTest<AssignParser, AssignToken> {
    public override string ParserSymbol { get { return "="; } }
}