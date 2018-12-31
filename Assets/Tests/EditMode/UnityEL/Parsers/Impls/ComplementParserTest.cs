using UnityEngine;
using UnityEditor;

public class ComplementParserTest : UnaryParserTest<ComplementParser, ComplementToken> {
    public override string ParserSymbol { get { return "~"; } }
}