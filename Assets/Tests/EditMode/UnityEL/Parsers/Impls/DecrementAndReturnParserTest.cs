using UnityEngine;
using UnityEditor;

public class DecrementAndReturnParserTest : UnaryParserTest<DecrementAndReturnParser, DecrementAndReturnToken> {
    public override string ParserSymbol { get { return "--"; } }
}