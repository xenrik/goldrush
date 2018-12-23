using UnityEngine;
using UnityEditor;

public class UnaryMinusParserTest : UnaryParserTest<UnaryMinusParser, UnaryMinusToken> {
    public override string ParserSymbol { get { return "-"; } }
}