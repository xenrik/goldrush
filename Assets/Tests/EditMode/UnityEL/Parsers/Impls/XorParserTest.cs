using UnityEngine;
using UnityEditor;

public class XorParserTest : BinaryParserTest<XorParser, XorToken> {
    public override string ParserSymbol { get { return "^"; } }
}