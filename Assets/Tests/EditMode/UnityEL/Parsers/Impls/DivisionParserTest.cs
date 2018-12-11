using UnityEngine;
using UnityEditor;

public class DivisionParserTest : BinaryParserTest<DivisionParser, DivisionToken> {
    public override string ParserSymbol { get { return "/"; } }
}