using UnityEngine;
using UnityEditor;

public class ShiftLeftParserTest : BinaryParserTest<ShiftLeftParser, ShiftLeftToken> {
    public override string ParserSymbol { get { return "<<"; } }
}