using UnityEngine;
using UnityEditor;

public class SubtractionParserTest : BinaryParserTest<SubtractionParser, SubtractionToken> {
    public override string ParserSymbol { get { return "-"; } }
}