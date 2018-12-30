using UnityEngine;
using UnityEditor;

public class ExistsParserTest : UnaryParserTest<ExistsParser, ExistsToken> {
    public override string ParserSymbol { get { return "exists"; } }
    public override bool SymbolRequiresTrailingSpace { get { return true; } }
}