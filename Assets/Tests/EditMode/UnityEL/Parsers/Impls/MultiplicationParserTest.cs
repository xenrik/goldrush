using UnityEngine;
using UnityEditor;

public class MultiplicationParserTest : BinaryParserTest<MultiplicationParser, MultiplicationToken> {
    public override string ParserSymbol { get { return "*"; } }
}