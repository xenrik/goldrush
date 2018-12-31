using UnityEngine;
using UnityEditor;

public class GreaterThanParserTest : BinaryParserTest<GreaterThanParser, GreaterThanToken> {
    public override string ParserSymbol { get { return ">"; } }
}