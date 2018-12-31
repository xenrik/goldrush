using UnityEngine;
using UnityEditor;

public class LessThanParserTest : BinaryParserTest<LessThanParser, LessThanToken> {
    public override string ParserSymbol { get { return "<"; } }
}