﻿using UnityEngine;
using UnityEditor;

public class BitwiseAndParserTest : BinaryParserTest<BitwiseAndParser, BitwiseAndToken> {
    public override string ParserSymbol { get { return "&"; } }
}