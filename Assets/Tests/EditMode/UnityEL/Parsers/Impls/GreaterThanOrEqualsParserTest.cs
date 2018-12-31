using UnityEngine;
using UnityEditor;

public class GreaterThanOrEqualsParserTest : BinaryParserTest<GreaterThanOrEqualsParser, GreaterThanOrEqualsToken> {
    public override string ParserSymbol { get { return ">="; } }
}