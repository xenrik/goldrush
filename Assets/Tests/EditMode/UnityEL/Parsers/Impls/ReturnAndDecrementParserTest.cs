using UnityEngine;
using UnityEditor;

public class ReturnAndDecrementParserTest : LeftHandUnaryParserTest<ReturnAndDecrementParser, ReturnAndDecrementToken> {
    public override string ParserSymbol { get { return "--"; } }
}