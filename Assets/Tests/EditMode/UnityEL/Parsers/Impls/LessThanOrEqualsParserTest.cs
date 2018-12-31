using UnityEngine;
using UnityEditor;

public class LessThanOrEqualsParserTest : BinaryParserTest<LessThanOrEqualsParser, LessThanOrEqualsToken> {
    public override string ParserSymbol { get { return "<="; } }
}