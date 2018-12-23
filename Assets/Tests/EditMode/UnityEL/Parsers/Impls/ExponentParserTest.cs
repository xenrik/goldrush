using UnityEngine;
using UnityEditor;

public class ExponentParserTest : BinaryParserTest<ExponentParser, ExponentToken> {
    public override string ParserSymbol { get { return "**"; } }
}