using UnityEngine;
using UnityEditor;

public class AndParserTest : BinaryParserTest<AndParser, AndToken> {
    public override string ParserSymbol { get { return "&&"; } }
}