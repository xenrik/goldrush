using UnityEngine;
using UnityEditor;

public class AsParserTest : BinaryParserTest<AsParser, AsToken> {
    public override string ParserSymbol { get { return "as"; } }
    public override bool SymbolRequiresTrailingSpace { get { return true; } }
}