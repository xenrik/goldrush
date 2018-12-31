using UnityEngine;
using UnityEditor;

public class ShiftRightParserTest : BinaryParserTest<ShiftRightParser, ShiftRightToken> {
    public override string ParserSymbol { get { return ">>"; } }
}