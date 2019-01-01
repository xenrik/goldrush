using UnityEngine;
using UnityEditor;

public class AddAndAssignParserTest : BinaryParserTest<AddAndAssignParser, AddAndAssignToken> {
    public override string ParserSymbol { get { return "+="; } }
}