using UnityEngine;
using UnityEditor;

public class BitwiseOrParserTest : BinaryParserTest<BitwiseOrParser, BitwiseOrToken> {
    public override string ParserSymbol { get { return "|"; } }
}