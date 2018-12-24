using UnityEngine;
using UnityEditor;

public class NullCoalesceParserTest : BinaryParserTest<NullCoalesceParser, NullCoalesceToken> {
    public override string ParserSymbol { get { return "??"; } }
}