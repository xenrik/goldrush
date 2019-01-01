using UnityEngine;
using UnityEditor;

public class MultiplyAndAssignParserTest : BinaryParserTest<MultiplyAndAssignParser, MultiplyAndAssignToken> {
    public override string ParserSymbol { get { return "*="; } }
}