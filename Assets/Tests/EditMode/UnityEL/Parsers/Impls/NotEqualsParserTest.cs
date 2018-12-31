using UnityEngine;
using UnityEditor;

public class NotEqualsParserTest : BinaryParserTest<NotEqualsParser, NotEqualsToken> {
    public override string ParserSymbol { get { return "!="; } }
}