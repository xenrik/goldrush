using UnityEngine;
using UnityEditor;

public class AdditionParserTest : BinaryParserTest<AdditionParser, AdditionToken> {
    public override string ParserSymbol { get { return "+"; } }
}