public class SubtractAndAssignParserTest : BinaryParserTest<SubtractAndAssignParser, SubtractAndAssignToken> {
    public override string ParserSymbol { get { return "-="; } }
}