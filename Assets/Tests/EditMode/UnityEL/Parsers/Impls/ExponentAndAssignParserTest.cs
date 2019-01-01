using UnityEngine;
using UnityEditor;

public class ExponentAndAssignParserTest : BinaryParserTest<ExponentAndAssignParser, ExponentAndAssignToken> {
    public override string ParserSymbol { get { return "**="; } }
}