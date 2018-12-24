using UnityEngine;
using UnityEditor;

public class OrParserTest : BinaryParserTest<OrParser, OrToken> {
    public override string ParserSymbol { get { return "||"; } }
}