public class AddAndAssignParserTest : BinaryParserTest<AddAndAssignParser, AddAndAssignToken> {
    public override string ParserSymbol { get { return "+="; } }
}